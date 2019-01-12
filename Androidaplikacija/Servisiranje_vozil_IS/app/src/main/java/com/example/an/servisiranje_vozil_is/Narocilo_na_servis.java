package com.example.an.servisiranje_vozil_is;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;
import android.util.Log;
import android.text.TextUtils;

import com.android.volley.NetworkResponse;
import com.android.volley.RequestQueue;
import com.android.volley.VolleyError;
import com.android.volley.Response;
import com.android.volley.Request;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.HttpHeaderParser;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;
import com.android.volley.toolbox.StringRequest;

import org.json.JSONArray;
import org.json.JSONObject;
import org.json.JSONException;

import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class Narocilo_na_servis extends Activity {
    private String uporabnisko_ime;
    private  String geslo;
    private  String id_uporabnika;
    private  String ime;
    private  String priimek;
    private  String sekundarni_id;
    private  String stanje;
    private Spinner poslovalnice;
    private  Spinner vozila;
    private  Spinner ura;
    private  Spinner minuta;
    private  Spinner dan;
    private  Spinner mesec;
    private  Spinner leto;
    private EditText opis;
    RequestQueue requestQueue;
    ArrayList<String>  seznam_poslovalnic = new ArrayList<String>();
    ArrayList<String> id_poslovalnic = new ArrayList<String>();
    ArrayList<String> seznam_vozil = new ArrayList<String>();
    ArrayList<String> id_vozil = new ArrayList<String>();
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_narocilo_na_servis);
        Intent intent = getIntent();
        uporabnisko_ime = intent.getStringExtra("uporabnisko_ime");
        geslo = intent.getStringExtra("geslo");
        id_uporabnika = intent.getStringExtra("id_uporabnika");
        ime = intent.getStringExtra("ime");
        priimek = intent.getStringExtra("priimek");
        sekundarni_id = intent.getStringExtra("sekundarni_id");
        stanje = intent.getStringExtra("stanje");
        poslovalnice = (Spinner)findViewById(R.id.spinner5);
        vozila = (Spinner)findViewById(R.id.spinner6);
        ura = (Spinner)findViewById(R.id.spinner7);
        minuta = (Spinner)findViewById(R.id.spinner8);
        dan = (Spinner)findViewById(R.id.spinner9);
        mesec = (Spinner)findViewById(R.id.spinner10);
        leto = (Spinner)findViewById(R.id.spinner11);
        opis = (EditText)findViewById(R.id.editText9);
        requestQueue = Volley.newRequestQueue(this);
        dobi_poslovalnice();
        dobi_vozila();
        dodaj_pravilne_datume();
    }

    public void iz_naroc_na_meni(android.view.View v){
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
    public void dobi_poslovalnice(){
        String url = "http://serviseranjevozil20180809013812.azurewebsites.net/Service1.svc/Get_poslovalnice";
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
                                    seznam_poslovalnic.add(jsonObj.get("naziv").toString() + ", " + jsonObj.get("kraj").toString());
                                    id_poslovalnic.add(jsonObj.get("id_poslovalnice").toString());
                                } catch (JSONException e) {
                                    // If there is an error then output this to the logs.
                                    Log.e("Volley", "Invalid JSON Object.");
                                }

                            }
                            String[] poslovalnice_array = new String[seznam_poslovalnic.size()];
                            poslovalnice_array = seznam_poslovalnic.toArray(poslovalnice_array);
                            ArrayAdapter<String> adapter = new ArrayAdapter<String>(getApplicationContext(),
                                    android.R.layout.simple_spinner_item, poslovalnice_array);
                            adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                            poslovalnice.setAdapter(adapter);

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
    public void dobi_vozila(){
        String url = "http://serviseranjevozil20180809013812.azurewebsites.net/Service1.svc/Get_vozila/" + sekundarni_id;
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
                                    seznam_vozil.add(jsonObj.get("znamka").toString() + " " + jsonObj.get("model").toString() + " " + jsonObj.get("letnica"));
                                    id_vozil.add(jsonObj.get("id_vozila").toString());
                                } catch (JSONException e) {
                                    // If there is an error then output this to the logs.
                                    Log.e("Volley", "Invalid JSON Object.");
                                }

                            }
                            String[] znamke_array = new String[seznam_vozil.size()];
                            znamke_array = seznam_vozil.toArray(znamke_array);
                            ArrayAdapter<String> adapter = new ArrayAdapter<String>(getApplicationContext(),
                                    android.R.layout.simple_spinner_item, znamke_array);
                            adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
                            vozila.setAdapter(adapter);

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
        ) {

            //This is for Headers If You Needed
            @Override
            public Map<String, String> getHeaders() {
                Map<String, String> params = new HashMap<String, String>();
                params.put("Content-Type", "application/json; charset=UTF-8");
                params.put("Authorization", uporabnisko_ime + ":" + geslo);
                return params;
            }
        };
        // Add the request we just defined to our request queue.
        // The request queue will automatically handle the request as soon as it can.
        requestQueue.add(arrReq);
    }

    public void dodaj_pravilne_datume(){
        String[] dnevi = new String[31];
        for(int i = 1; i <= 31;i++){
            dnevi[i-1] = String.valueOf(i);
        }
        ArrayAdapter<String> adapter = new ArrayAdapter<String>(getApplicationContext(),
                android.R.layout.simple_spinner_item, dnevi);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        dan.setAdapter(adapter);


        String[] meseci = new String[12];
        for(int i = 1; i <= 12;i++){
            meseci[i-1] = String.valueOf(i);
        }
        ArrayAdapter<String> adapter1 = new ArrayAdapter<String>(getApplicationContext(),
                android.R.layout.simple_spinner_item, meseci);
        adapter1.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        mesec.setAdapter(adapter1);


        String[] leta = new String[82];
        for(int i = 2019; i <= 2100;i++){
            leta[i-2019] = String.valueOf(i);
        }
        ArrayAdapter<String> adapter2 = new ArrayAdapter<String>(getApplicationContext(),
                android.R.layout.simple_spinner_item, leta);
        adapter2.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        leto.setAdapter(adapter2);


        String[] ure = new String[24];
        for(int i = 0; i <= 23;i++){
            ure[i] = String.valueOf(i);
        }
        ArrayAdapter<String> adapter3 = new ArrayAdapter<String>(getApplicationContext(),
                android.R.layout.simple_spinner_item, ure);
        adapter3.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        ura.setAdapter(adapter3);


        String[] minute = new String[60];
        for(int i = 0; i <= 59;i++){
            minute[i] = String.valueOf(i);
        }
        ArrayAdapter<String> adapter4 = new ArrayAdapter<String>(getApplicationContext(),
                android.R.layout.simple_spinner_item, minute);
        adapter4.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        minuta.setAdapter(adapter4);
    }

    public void pri_na_ser(android.view.View v){
        Context context = getApplicationContext();
        int duration = Toast.LENGTH_SHORT;
        if(opis.getText().toString().equals("")) {
            CharSequence text = "Prosim izpolnite vsa polja!";
            Toast toast = Toast.makeText(context, text, duration);
            toast.show();
            return;
        }
        String url = "http://serviseranjevozil20180809013812.azurewebsites.net/Service1.svc/Naroci";
        int voziloid = vozila.getSelectedItemPosition();
        int poslovalnicaid = poslovalnice.getSelectedItemPosition();
        String ura_narocila = ura.getSelectedItem().toString();
        String minuta_narocila = minuta.getSelectedItem().toString();
        String dan_narocila = dan.getSelectedItem().toString();
        String mesec_narocila = mesec.getSelectedItem().toString();
        String leto_narocila = leto.getSelectedItem().toString();
        String opis_narocila = opis.getText().toString();
        try {
            JSONObject jsonBody = new JSONObject();
            jsonBody.put("id_vozila", id_vozil.get(voziloid));
            jsonBody.put("id_poslovalnice", id_poslovalnic.get(poslovalnicaid));
            jsonBody.put("ura", ura_narocila);
            jsonBody.put("minuta", minuta_narocila);
            jsonBody.put("dan", dan_narocila);
            jsonBody.put("mesec", mesec_narocila);
            jsonBody.put("leto", leto_narocila);
            jsonBody.put("opis", opis_narocila);
            final String mRequestBody = jsonBody.toString();

            StringRequest stringRequest = new StringRequest(Request.Method.POST, url, new Response.Listener<String>() {
                @Override
                public void onResponse(String response) {
                    try{
                        response = response.replace("\"", "");
                        if(response.equals("Vse je vredu!")) {
                            Context context = getApplicationContext();
                            int duration = Toast.LENGTH_SHORT;
                            CharSequence text = "Narocilo je bilo uspešno oddano!";
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
