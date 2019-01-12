package com.example.an.servisiranje_vozil_is;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.widget.EditText;
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

import java.util.HashMap;
import java.util.Map;

public class Prijava extends Activity {

    private EditText uporabnisko_ime;
    private EditText geslo;
    RequestQueue requestQueue;
    String url = "http://serviseranjevozil20180809013812.azurewebsites.net/Service1.svc/Get_oseba";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_prijava);
        uporabnisko_ime = (EditText)findViewById(R.id.editText8);
        geslo = (EditText)findViewById(R.id.editText10);
        requestQueue = Volley.newRequestQueue(this);
    }

    public void nazaj_na_zacetek(android.view.View v){
        finish();
        Intent homepage = new Intent(Prijava.this, MainActivity.class);
        startActivity(homepage);
    }

    public void prijavi_osebo(android.view.View v){
        Context context = getApplicationContext();
        int duration = Toast.LENGTH_SHORT;
        if(TextUtils.isEmpty(uporabnisko_ime.getText().toString()) || TextUtils.isEmpty(geslo.getText().toString())){
            CharSequence text = "Vpisite vse podatke!";
            Toast toast = Toast.makeText(context, text, duration);
            toast.show();
            return;
        }
        JsonObjectRequest arrReq = new JsonObjectRequest(Request.Method.GET, url,
                new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {
                        // Check the length of our response (to see if the user has any repos)
                        if (response.length() > 0) {
                            // The user does have repos, so let's loop through them all.
                                try {
                                    // For each repo, add a new line to our repo list.

                                    JSONObject jsonObj = response;
                                    String id_poslovalnice = jsonObj.get("id_poslovalnice").toString();
                                    String id_uporabnika = jsonObj.get("id_uporabnika").toString();
                                    String ime = jsonObj.get("ime").toString();
                                    String priimek = jsonObj.get("priimek").toString();
                                    String sekundarni_id = jsonObj.get("sekundarni_id").toString();
                                    String stanje = jsonObj.get("stanje").toString();
                                    Context context = getApplicationContext();
                                    int duration = Toast.LENGTH_SHORT;
                                    if(ime.equals("Napačno uporabniško ime ali geslo")) {
                                        CharSequence text = "Napačno uporabniško ime ali geslo!";
                                        Toast toast = Toast.makeText(context, text, duration);
                                        toast.show();
                                    }
                                    else{
                                        CharSequence text = "Pozdravljen " + ime + " " + priimek;
                                        Toast toast = Toast.makeText(context, text, duration);
                                        toast.show();
                                        if(stanje.equals("2")){
                                            finish();
                                            Intent i=new Intent(context,Meni_za_zaposlene.class);
                                            i.putExtra("uporabnisko_ime", uporabnisko_ime.getText().toString());
                                            i.putExtra("geslo", geslo.getText().toString());
                                            i.putExtra("id_poslovalnice", id_poslovalnice);
                                            i.putExtra("id_uporabnika", id_uporabnika);
                                            i.putExtra("ime", ime);
                                            i.putExtra("priimek", priimek);
                                            i.putExtra("sekundarni_id", sekundarni_id);
                                            i.putExtra("stanje", stanje);
                                            context.startActivity(i);
                                        }
                                        else{
                                            if(stanje.equals("1")) {
                                                finish();
                                                Intent i=new Intent(context,Meni_za_stranke.class);
                                                i.putExtra("uporabnisko_ime", uporabnisko_ime.getText().toString());
                                                i.putExtra("geslo", geslo.getText().toString());
                                                i.putExtra("id_uporabnika", id_uporabnika);
                                                i.putExtra("ime", ime);
                                                i.putExtra("priimek", priimek);
                                                i.putExtra("sekundarni_id", sekundarni_id);
                                                i.putExtra("stanje", stanje);
                                                context.startActivity(i);
                                            }
                                        }

                                    }
                                } catch (JSONException e) {
                                    // If there is an error then output this to the logs.
                                    Log.e("Volley", "Invalid JSON Object.");
                                    Context context = getApplicationContext();
                                    int duration = Toast.LENGTH_SHORT;
                                    CharSequence text = "Prislo je do napake: " + e.toString();
                                    Toast toast = Toast.makeText(context, text, duration);
                                    toast.show();
                                }


                        } else {
                            // The user didn't have any repos.
                            Context context = getApplicationContext();
                            int duration = Toast.LENGTH_SHORT;
                            CharSequence text = "Odgovor je bil prazen!";
                            Toast toast = Toast.makeText(context, text, duration);
                            toast.show();
                        }

                    }
                },

                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        // If there a HTTP error then add a note to our repo list.
                        Context context = getApplicationContext();
                        int duration = Toast.LENGTH_SHORT;
                        CharSequence text = "Napaka: " + error.toString();
                        Toast toast = Toast.makeText(context, text, duration);
                        toast.show();
                    }
                }
        ) {

            //This is for Headers If You Needed
            @Override
            public Map<String, String> getHeaders() {
                Map<String, String> params = new HashMap<String, String>();
                params.put("Content-Type", "application/json; charset=UTF-8");
                params.put("Authorization", uporabnisko_ime.getText().toString() + ":" + geslo.getText().toString());
                return params;
            }
        };
        // Add the request we just defined to our request queue.
        // The request queue will automatically handle the request as soon as it can.
        requestQueue.add(arrReq);
    }


}
