package com.example.an.servisiranje_vozil_is;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.ScrollView;
import android.widget.Toast;
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

public class Brisi_vozilo extends Activity {
    private String uporabnisko_ime;
    private  String geslo;
    private  String id_uporabnika;
    private  String ime;
    private  String priimek;
    private  String sekundarni_id;
    private  String stanje;
    ScrollView hsv1;
    LinearLayout layout;
    RequestQueue requestQueue;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_brisi_vozilo);
        Intent intent = getIntent();
        uporabnisko_ime = intent.getStringExtra("uporabnisko_ime");
        geslo = intent.getStringExtra("geslo");
        id_uporabnika = intent.getStringExtra("id_uporabnika");
        ime = intent.getStringExtra("ime");
        priimek = intent.getStringExtra("priimek");
        sekundarni_id = intent.getStringExtra("sekundarni_id");
        stanje = intent.getStringExtra("stanje");
        requestQueue = Volley.newRequestQueue(this);
        hsv1 = (ScrollView) findViewById( R.id.scrollView );
        layout = (LinearLayout) hsv1.findViewById( R.id.hsvLayout1 );
        layout.removeAllViews();
        dobi_vozila();

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
                                        Button myButton = new Button (getApplicationContext());
                                        LinearLayout.LayoutParams lp = new LinearLayout.LayoutParams( LinearLayout.LayoutParams.MATCH_PARENT, LinearLayout.LayoutParams.MATCH_PARENT );
                                        lp.setMargins( 20, 0, 20, 0 );
                                        myButton.setLayoutParams(lp);
                                        myButton.setId(Integer.parseInt(jsonObj.get("id_vozila").toString()));
                                        myButton.setOnClickListener(new View.OnClickListener() {
                                            public void onClick(View v) {
                                                izbrisi_vozilo(v.getId());
                                            }
                                        });
                                        myButton.setTextColor( Color.YELLOW );
                                        myButton.setGravity( Gravity.CENTER_VERTICAL | Gravity.CENTER_HORIZONTAL );
                                        myButton.setText( jsonObj.get("znamka").toString() + " " + jsonObj.get("model").toString() + " " + jsonObj.get("letnica") );
                                        layout.addView(myButton);

                                } catch (JSONException e) {
                                    // If there is an error then output this to the logs.
                                    Log.e("Volley", "Invalid JSON Object.");
                                }

                            }


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

    public void iz_brisa_na_meni(android.view.View v){
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

    public void izbrisi_vozilo(int idv){
        String id = String.valueOf(idv);
        String url = "http://serviseranjevozil20180809013812.azurewebsites.net/Service1.svc/Izbrisi_vozilo/" + id;

            StringRequest stringRequest = new StringRequest(Request.Method.DELETE, url, new Response.Listener<String>() {
                @Override
                public void onResponse(String response) {
                    try{
                        response = response.replace("\"", "");
                        if(response.equals("Vse je vredu!")) {
                            Context context = getApplicationContext();
                            int duration = Toast.LENGTH_SHORT;
                            CharSequence text = "Vozilo je bilo uspešno izbrisano!";
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
                        return null;
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

    }
}
