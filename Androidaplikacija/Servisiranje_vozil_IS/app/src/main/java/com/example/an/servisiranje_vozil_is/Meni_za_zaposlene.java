package com.example.an.servisiranje_vozil_is;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.widget.Toast;

public class Meni_za_zaposlene extends Activity {
    private String uporabnisko_ime;
    private  String geslo;
    private  String id_uporabnika;
    private  String ime;
    private  String priimek;
    private  String sekundarni_id;
    private  String stanje;
    private  String id_poslovalnice;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_meni_za_zaposlene);
        Intent intent = getIntent();
        uporabnisko_ime = intent.getStringExtra("uporabnisko_ime");
        geslo = intent.getStringExtra("geslo");
        id_uporabnika = intent.getStringExtra("id_uporabnika");
        ime = intent.getStringExtra("ime");
        priimek = intent.getStringExtra("priimek");
        sekundarni_id = intent.getStringExtra("sekundarni_id");
        stanje = intent.getStringExtra("stanje");
        id_poslovalnice = intent.getStringExtra("id_poslovalnice");

    }

    public void odobr_naroc(android.view.View v){
        Context context = getApplicationContext();
        finish();
        Intent i=new Intent(context,Potrdi_narocilo.class);
        i.putExtra("uporabnisko_ime", uporabnisko_ime);
        i.putExtra("geslo", geslo);
        i.putExtra("id_uporabnika", id_uporabnika);
        i.putExtra("ime", ime);
        i.putExtra("priimek", priimek);
        i.putExtra("sekundarni_id", sekundarni_id);
        i.putExtra("stanje", stanje);
        i.putExtra("id_poslovalnice",id_poslovalnice);
        context.startActivity(i);
    }

    public void bris_naroc(android.view.View v){
        Context context = getApplicationContext();
        finish();
        Intent i=new Intent(context,Brisi_narocilo_poslovalnice.class);
        i.putExtra("uporabnisko_ime", uporabnisko_ime);
        i.putExtra("geslo", geslo);
        i.putExtra("id_uporabnika", id_uporabnika);
        i.putExtra("ime", ime);
        i.putExtra("priimek", priimek);
        i.putExtra("sekundarni_id", sekundarni_id);
        i.putExtra("stanje", stanje);
        i.putExtra("id_poslovalnice",id_poslovalnice);
        context.startActivity(i);
    }

    public void oprav_naroc(android.view.View v){
        Context context = getApplicationContext();
        finish();
        Intent i=new Intent(context,Opravi_narocilo.class);
        i.putExtra("uporabnisko_ime", uporabnisko_ime);
        i.putExtra("geslo", geslo);
        i.putExtra("id_uporabnika", id_uporabnika);
        i.putExtra("ime", ime);
        i.putExtra("priimek", priimek);
        i.putExtra("sekundarni_id", sekundarni_id);
        i.putExtra("stanje", stanje);
        i.putExtra("id_poslovalnice",id_poslovalnice);
        context.startActivity(i);
    }
}
