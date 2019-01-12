package com.example.an.servisiranje_vozil_is;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.Toast;

import com.android.volley.NetworkResponse;
import com.android.volley.RequestQueue;
import com.android.volley.VolleyError;
import com.android.volley.Response;
import com.android.volley.Request;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.HttpHeaderParser;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONObject;
import org.json.JSONException;

import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class Dodajanje_vozil extends Activity {
    private String uporabnisko_ime;
    private  String geslo;
    private  String id_uporabnika;
    private  String ime;
    private  String priimek;
    private  String sekundarni_id;
    private  String stanje;
    private Spinner znamka;
    private Spinner model;
    private Spinner letnik;
    ArrayList<String> znamke = new ArrayList<String>();
    ArrayList<String> id_znamke = new ArrayList<String>();
    ArrayList<String> modeli_vozil = new ArrayList<String>();
    ArrayList<String> id_modelov = new ArrayList<String>();
    RequestQueue requestQueue;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_dodajanje_vozil);
        Intent intent = getIntent();
        uporabnisko_ime = intent.getStringExtra("uporabnisko_ime");
        geslo = intent.getStringExtra("geslo");
        id_uporabnika = intent.getStringExtra("id_uporabnika");
        ime = intent.getStringExtra("ime");
        priimek = intent.getStringExtra("priimek");
        sekundarni_id = intent.getStringExtra("sekundarni_id");
        stanje = intent.getStringExtra("stanje");
        znamka = (Spinner)findViewById(R.id.spinner);
        model = (Spinner)findViewById(R.id.spinner2);
        letnik = (Spinner)findViewById(R.id.spinner3);
        requestQueue = Volley.newRequestQueue(this);
        dodaj_znamke();
        dodaj_letnice();
        znamka.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parentView, View selectedItemView, int position, long id) {
                // your code here
                model.setAdapter(null);
                dodaj_modele(position);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parentView) {
                // your code here
            }

        });
    }


    public void na_meni_stranke(android.view.View v){
        Context context = getApplicationContext();
        finish();
        Intent i=new Intent(context,Meni_za_stranke.class);
        i.putExtra("uporabnisko_ime", uporabnisko_ime);
        i.putExtra("geslo", geslo);
        i.putExtra("id_uporabnika", id_uporabnika);
        i.putExtra("ime", ime);
        i.putExtra("priimek", priimek);
        i.putExtra("sekundarni_id", sekundarni_id);
        i.putExtra("stanje", stanje);
        context.startActivity(i);
    }

    public void dodaj_znamke(){
        String url = "http://serviseranjevozil20180809013812.azurewebsites.net/Service1.svc/Get_znamke";
        JsonArrayRequest arrReq = new JsonArrayRequest(Request.Method.GET, url,
                new Response.Listener<JSONArray>() {
                    @Override
                    public void onResponse(JSONArray response) {
                        // Check the length of our response (to see if the user has any repos)
                        if (response.length() > 0) {
                            // The user does have repos, so let's loop through them all.
                            for (int i = 0; i < response.length(); i++) {
                                try {
                                    // For each repo, add a new line to our repo list.
                                    JSONObject jsonObj = response.getJSONObject(i);
                                     znamke.add(jsonObj.get("znamka").toString());
                                     id_znamke.add(jsonObj.get("id_znamke").toString());
                                } catch (JSONException e) {
                                    // If there is an error then output this to the logs.
                                    Log.e("Volley", "Invalid JSON Object.");
                                }

                            }
                            String[] znamke_array = new String[znamke.size()];
                            znamke_array = znamke.toArray(znamke_array);
                            ArrayAdapter<String> adapter = new ArrayAdapter<String>(getApplicationContext(),
                                    android.R.layout.simple_spinner_item, znamke_array);
                            adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                            znamka.setAdapter(adapter);

                        } else {
                            // The user didn't have any repos.
                        }

                    }
                },

                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        // If there a HTTP error then add a note to our repo list.
                        Log.e("Volley", error.toString());
                    }
                }
        );
        // Add the request we just defined to our request queue.
        // The request queue will automatically handle the request as soon as it can.
        requestQueue.add(arrReq);
    }

    public void dodaj_letnice(){
        String[] letnice = new String[201];
        for(int i = 1900; i <= 2100;i++){
            letnice[i-1900] = String.valueOf(i);
        }
        ArrayAdapter<String> adapter = new ArrayAdapter<String>(getApplicationContext(),
                android.R.layout.simple_spinner_item, letnice);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        letnik.setAdapter(adapter);

    }
    public void dodaj_modele(int position){
        modeli_vozil = new ArrayList<String>();
        id_modelov = new ArrayList<String>();
        String id_modela = String.valueOf(id_znamke.get(position));
        String url = "http://serviseranjevozil20180809013812.azurewebsites.net/Service1.svc/Get_modeli/" + id_modela;
        JsonArrayRequest arrReq = new JsonArrayRequest(Request.Method.GET, url,
                new Response.Listener<JSONArray>() {
                    @Override
                    public void onResponse(JSONArray response) {
                        // Check the length of our response (to see if the user has any repos)
                        if (response.length() > 0) {
                            // The user does have repos, so let's loop through them all.
                            for (int i = 0; i < response.length(); i++) {
                                try {
                                    // For each repo, add a new line to our repo list.
                                    JSONObject jsonObj = response.getJSONObject(i);
                                    modeli_vozil.add(jsonObj.get("model").toString());
                                    id_modelov.add(jsonObj.get("id_modela").toString());
                                } catch (JSONException e) {
                                    // If there is an error then output this to the logs.
                                    Log.e("Volley", "Invalid JSON Object.");
                                }

                            }
                            String[] modeli_array = new String[modeli_vozil.size()];
                            modeli_array = modeli_vozil.toArray(modeli_array);
                            ArrayAdapter<String> adapter = new ArrayAdapter<String>(getApplicationContext(),
                                    android.R.layout.simple_spinner_item, modeli_array);
                            adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                            model.setAdapter(adapter);

                        } else {
                            // The user didn't have any repos.
                        }

                    }
                },

                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        // If there a HTTP error then add a note to our repo list.
                        Log.e("Volley", error.toString());
                    }
                }
        );
        // Add the request we just defined to our request queue.
        // The request queue will automatically handle the request as soon as it can.
        requestQueue.add(arrReq);

    }

    public void dodaj_vozilo(android.view.View v){
        String url = "http://serviseranjevozil20180809013812.azurewebsites.net/Service1.svc/Dodaj_vozilo";
        int znamkaid = znamka.getSelectedItemPosition();
        int modelid = model.getSelectedItemPosition();
        String letnicaVozila = letnik.getSelectedItem().toString();
        Context context = getApplicationContext();
        int duration = Toast.LENGTH_SHORT;
        try {
            JSONObject jsonBody = new JSONObject();
            jsonBody.put("id_znamke", id_znamke.get(znamkaid));
            jsonBody.put("id_modela", id_modelov.get(modelid));
            jsonBody.put("letnica", letnicaVozila);
            final String mRequestBody = jsonBody.toString();

            StringRequest stringRequest = new StringRequest(Request.Method.POST, url, new Response.Listener<String>() {
                @Override
                public void onResponse(String response) {
                    try{
                        response = response.replace("\"", "");
                        if(response.equals("Vse je vredu!")) {
                            Context context = getApplicationContext();
                            int duration = Toast.LENGTH_SHORT;
                            CharSequence text = "Vozilo je bilo uspešno dodano!";
                            Toast toast = Toast.makeText(context, text, duration);
                            toast.show();
                            finish();
                            Intent i=new Intent(context,Meni_za_stranke.class);
                            i.putExtra("uporabnisko_ime", uporabnisko_ime);
                            i.putExtra("geslo", geslo);
                            i.putExtra("id_uporabnika", id_uporabnika);
                            i.putExtra("ime", ime);
                            i.putExtra("priimek", priimek);
                            i.putExtra("sekundarni_id", sekundarni_id);
                            i.putExtra("stanje", stanje);
                            context.startActivity(i);
                        }
                        else{
                            Context context = getApplicationContext();
                            int duration = Toast.LENGTH_SHORT;
                            CharSequence text1 = response;
                            Toast toast = Toast.makeText(context, text1, duration);
                            toast.show();
                        }
                    }
                    catch (Exception e) {
                        Context context = getApplicationContext();
                        int duration = Toast.LENGTH_SHORT;
                        CharSequence text = "Prislo je do napake: " + e.toString();
                        Toast toast = Toast.makeText(context, text, duration);
                        toast.show();
                    }
                }
            }, new Response.ErrorListener() {
                @Override
                public void onErrorResponse(VolleyError error) {
                    Context context = getApplicationContext();
                    int duration = Toast.LENGTH_SHORT;
                    CharSequence text = "Prislo je do napake: " + error.toString() + "! " + error.getMessage();
                    Toast toast = Toast.makeText(context, text, duration);
                    toast.show();
                    Log.e("LOG_VOLLEY", error.toString());
                }
            }) {

                @Override
                public Map<String, String> getHeaders() {
                    Map<String, String> params = new HashMap<String, String>();
                    params.put("Authorization", uporabnisko_ime + ":" + geslo);
                    return params;
                }

                @Override
                public String getBodyContentType() {
                    return "application/json; charset=utf-8";
                }

                @Override
                public byte[] getBody(){
                    try {
                        return mRequestBody == null ? null : mRequestBody.getBytes("utf-8");
                    } catch (UnsupportedEncodingException uee) {
                        Context context = getApplicationContext();
                        int duration = Toast.LENGTH_SHORT;
                        CharSequence text = "Prislo je do napake v kodiranje: " + uee.toString();
                        Toast toast = Toast.makeText(context, text, duration);
                        toast.show();
                        VolleyLog.wtf("Unsupported Encoding while trying to get the bytes of %s using %s", mRequestBody, "utf-8");
                        return null;
                    }
                }

                @Override
                protected Response<String> parseNetworkResponse(NetworkResponse response) {
                    String responseString = "";
                    if (response != null) {
                        String neki =  new String(response.data);
                        responseString = neki;

                    }
                    return Response.success(responseString, HttpHeaderParser.parseCacheHeaders(response));
                }
            };

            requestQueue.add(stringRequest);
        } catch (JSONException e) {
            CharSequence text1 = "Prislo je do napake: " + e.toString() + "!";
            Toast toast1 = Toast.makeText(context, text1, duration);
            toast1.show();
            e.printStackTrace();
        }
    }


}
