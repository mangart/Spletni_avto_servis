package com.example.an.servisiranje_vozil_is;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.text.TextUtils;
import android.widget.EditText;
import android.widget.Toast;
import android.util.Log;

import com.android.volley.NetworkResponse;
import com.android.volley.RequestQueue;
import com.android.volley.VolleyError;
import com.android.volley.Response;
import com.android.volley.Request;
import com.android.volley.VolleyLog;
import com.android.volley.toolbox.HttpHeaderParser;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.Volley;
import com.android.volley.toolbox.StringRequest;

import org.json.JSONArray;
import org.json.JSONObject;
import org.json.JSONException;

import org.w3c.dom.Text;

import java.io.UnsupportedEncodingException;

public class Registracija extends Activity {

    private EditText uporabnisko_ime;
    private EditText geslo;
    private EditText ponovnoGeslo;
    private EditText ime;
    private EditText priimek;
    private EditText telefon;
    private EditText email;
    RequestQueue requestQueue;
    String url = "http://serviseranjevozil20180809013812.azurewebsites.net/Service1.svc/Registracija";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registracija);
        uporabnisko_ime = (EditText)findViewById(R.id.editText);
        geslo = (EditText)findViewById(R.id.editText2);
        ponovnoGeslo = (EditText)findViewById(R.id.editText3);
        ime = (EditText)findViewById(R.id.editText4);
        priimek = (EditText)findViewById(R.id.editText5);
        telefon = (EditText)findViewById(R.id.editText7);
        email = (EditText)findViewById(R.id.editText6);
        requestQueue = Volley.newRequestQueue(this);
    }

    public void opravi_registracijo(android.view.View v){
        Context context = getApplicationContext();
        int duration = Toast.LENGTH_SHORT;
        if(TextUtils.isEmpty(uporabnisko_ime.getText().toString()) || TextUtils.isEmpty(geslo.getText().toString()) || TextUtils.isEmpty(ponovnoGeslo.getText().toString()) || TextUtils.isEmpty(ime.getText().toString()) || TextUtils.isEmpty(priimek.getText().toString()) || TextUtils.isEmpty(telefon.getText().toString()) || TextUtils.isEmpty(email.getText().toString())){
            CharSequence text = "Niste vnesli vseh podatkov!";
            Toast toast = Toast.makeText(context, text, duration);
            toast.show();
        }
        else{
            if(!geslo.getText().toString().equals(ponovnoGeslo.getText().toString())){
                CharSequence text = "Geslo se ne ujema s ponovnim geslom!";
                Toast toast = Toast.makeText(context, text, duration);
                toast.show();
            }
            else {
                if(!email.getText().toString().contains("@")){
                    CharSequence text = "Email ni pravilen!";
                    Toast toast = Toast.makeText(context, text, duration);
                    toast.show();
                }
                else{
                    naredi_zahtevek();
                }

            }
        }

    }

    public void naz(android.view.View v){
        finish();
        Intent homepage = new Intent(Registracija.this, MainActivity.class);
        startActivity(homepage);
    }

    private void naredi_zahtevek(){
        try {
            JSONObject jsonBody = new JSONObject();
            jsonBody.put("uporabnisko_ime", uporabnisko_ime.getText());
            jsonBody.put("geslo", geslo.getText());
            jsonBody.put("ime", ime.getText());
            jsonBody.put("priimek", priimek.getText());
            jsonBody.put("email", email.getText());
            jsonBody.put("telefon", telefon.getText());
            final String mRequestBody = jsonBody.toString();

            StringRequest stringRequest = new StringRequest(Request.Method.POST, url, new Response.Listener<String>() {
                @Override
                public void onResponse(String response) {
                    try{
                        int stevilo = Integer.parseInt(response);
                        if(stevilo == 48) {
                            Context context = getApplicationContext();
                            int duration = Toast.LENGTH_SHORT;
                            CharSequence text = "Registracija je bila uspesna!";
                            Toast toast = Toast.makeText(context, text, duration);
                            toast.show();
                        }
                        else{
                            Context context = getApplicationContext();
                            int duration = Toast.LENGTH_SHORT;
                            CharSequence text1 = "Prislo je do napake bodisi v bazi ali pa to uporabnisko ime ze obstaja!";
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
                    CharSequence text = "Prislo je do napake: " + error.toString() + "!";
                    Toast toast = Toast.makeText(context, text, duration);
                    toast.show();
                    Log.e("LOG_VOLLEY", error.toString());
                }
            }) {
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
                        int neki = response.data[0];
                        responseString = String.valueOf(neki);

                    }
                    return Response.success(responseString, HttpHeaderParser.parseCacheHeaders(response));
                }
            };

            requestQueue.add(stringRequest);
        } catch (JSONException e) {
            Context context = getApplicationContext();
            int duration = Toast.LENGTH_SHORT;
            CharSequence text = "Prislo je do napake: " + e.toString() + "!";
            Toast toast = Toast.makeText(context, text, duration);
            toast.show();
            e.printStackTrace();
        }
    }
}
