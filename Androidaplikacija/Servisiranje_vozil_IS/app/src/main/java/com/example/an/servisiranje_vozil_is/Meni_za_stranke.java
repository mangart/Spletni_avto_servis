package com.example.an.servisiranje_vozil_is;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Toast;

public class Meni_za_stranke extends Activity {

    private String uporabnisko_ime;
    private  String geslo;
    private  String id_uporabnika;
    private  String ime;
    private  String priimek;
    private  String sekundarni_id;
    private  String stanje;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_meni_za_stranke);
        Intent intent = getIntent();
        uporabnisko_ime = intent.getStringExtra("uporabnisko_ime");
        geslo = intent.getStringExtra("geslo");
        id_uporabnika = intent.getStringExtra("id_uporabnika");
        ime = intent.getStringExtra("ime");
        priimek = intent.getStringExtra("priimek");
        sekundarni_id = intent.getStringExtra("sekundarni_id");
        stanje = intent.getStringExtra("stanje");

    }

    public void dod_voz(android.view.View v){
        Context context = getApplicationContext();
        finish();
        Intent i=new Intent(context,Dodajanje_vozil.class);
        i.putExtra("uporabnisko_ime", uporabnisko_ime);
        i.putExtra("geslo", geslo);
        i.putExtra("id_uporabnika", id_uporabnika);
        i.putExtra("ime", ime);
        i.putExtra("priimek", priimek);
        i.putExtra("sekundarni_id", sekundarni_id);
        i.putExtra("stanje", stanje);
        context.startActivity(i);
    }

    public void naroc_na_ser(android.view.View v){
        Context context = getApplicationContext();
        finish();
        Intent i=new Intent(context,Narocilo_na_servis.class);
        i.putExtra("uporabnisko_ime", uporabnisko_ime);
        i.putExtra("geslo", geslo);
        i.putExtra("id_uporabnika", id_uporabnika);
        i.putExtra("ime", ime);
        i.putExtra("priimek", priimek);
        i.putExtra("sekundarni_id", sekundarni_id);
        i.putExtra("stanje", stanje);
        context.startActivity(i);
    }

    public void bris_vozil(View v){
        Context context = getApplicationContext();
        finish();
        Intent i=new Intent(context,Brisi_vozilo.class);
        i.putExtra("uporabnisko_ime", uporabnisko_ime);
        i.putExtra("geslo", geslo);
        i.putExtra("id_uporabnika", id_uporabnika);
        i.putExtra("ime", ime);
        i.putExtra("priimek", priimek);
        i.putExtra("sekundarni_id", sekundarni_id);
        i.putExtra("stanje", stanje);
        context.startActivity(i);
    }

    public void get_odob_ser(android.view.View v){
        Context context = getApplicationContext();
        finish();
        Intent i=new Intent(context,Pregled_odobrenih_servisov.class);
        i.putExtra("uporabnisko_ime", uporabnisko_ime);
        i.putExtra("geslo", geslo);
        i.putExtra("id_uporabnika", id_uporabnika);
        i.putExtra("ime", ime);
        i.putExtra("priimek", priimek);
        i.putExtra("sekundarni_id", sekundarni_id);
        i.putExtra("stanje", stanje);
        context.startActivity(i);
    }

    public void preg_opr_ser(android.view.View v){
        Context context = getApplicationContext();
        finish();
        Intent i=new Intent(context,Pregled_opravljenih_servisov.class);
        i.putExtra("uporabnisko_ime", uporabnisko_ime);
        i.putExtra("geslo", geslo);
        i.putExtra("id_uporabnika", id_uporabnika);
        i.putExtra("ime", ime);
        i.putExtra("priimek", priimek);
        i.putExtra("sekundarni_id", sekundarni_id);
        i.putExtra("stanje", stanje);
        context.startActivity(i);
    }
    public void brisi_naroc_na_servis(android.view.View v){
        Context context = getApplicationContext();
        finish();
        Intent i=new Intent(context,Brisi_narocilo_na_servis.class);
        i.putExtra("uporabnisko_ime", uporabnisko_ime);
        i.putExtra("geslo", geslo);
        i.putExtra("id_uporabnika", id_uporabnika);
        i.putExtra("ime", ime);
        i.putExtra("priimek", priimek);
        i.putExtra("sekundarni_id", sekundarni_id);
        i.putExtra("stanje", stanje);
        context.startActivity(i);
    }
}
