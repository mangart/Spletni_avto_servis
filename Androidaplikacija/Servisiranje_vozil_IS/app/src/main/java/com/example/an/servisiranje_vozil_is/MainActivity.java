package com.example.an.servisiranje_vozil_is;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;
import android.widget.Toast;

public class MainActivity extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
    }

    public void Registrac(android.view.View v){
        finish();
        Intent homepage = new Intent(MainActivity.this, Registracija.class);
        startActivity(homepage);
    }

    public void prija(android.view.View v){
        finish();
        Intent homepage = new Intent(MainActivity.this, Prijava.class);
        startActivity(homepage);
    }
}
