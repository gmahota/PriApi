package com.mahotaservicos.koattendance.api;

import android.util.JsonWriter;
import android.util.Log;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.io.StringWriter;

import okhttp3.FormBody;
import okhttp3.MediaType;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.RequestBody;
import okhttp3.Response;

public class DemoVolley {
    public OkHttpClient client ;

    private MediaType JSON = MediaType.parse("application/json");

    public  DemoVolley(){
        client = new OkHttpClient.Builder().build();
    }

    public String RequestQueue() throws IOException, JSONException {
        String token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1OTIxNTE2OTMsImV4cCI6MTU5MjIzODA5MywiaWF0IjoxNTkyMTUxNjkzfQ.dNoGdbE0HGmoOgmwcEUxMnMxD74u2RGygBWZaOBuKQw";

        JSONObject json = new JSONObject();
        json.put("DateBegin","2020-01-01T17:16:40");
        json.put("DateEnd","2020-12-31T17:16:40");
        json.put("Entity","");
        json.put("Type","FA");
        json.put("Reference","");

        RequestBody body = RequestBody.create(JSON,json.toString());

        Request request = new Request.Builder()
                .url("http://localhost:5000/api/Invoice")
                .addHeader("Authorization", token)
                .addHeader("Content-Type", "application/json")
                .method("Get", body)
                .build();


        try(Response response = client.newCall(request).execute()){
            Log.e("My_App",response.body().string());
        }


        return "";
    }

}
