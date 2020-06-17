package com.mahotaservicos.koattendance.api

import android.os.Build
import android.util.JsonWriter
import android.util.Log
import com.mahotaservicos.koattendance.BuildConfig
import okhttp3.MediaType.Companion.toMediaTypeOrNull
import okhttp3.OkHttpClient
import okhttp3.Request
import okhttp3.RequestBody
import okhttp3.ResponseBody
import java.io.Console
import java.io.StringWriter
import java.util.concurrent.TimeUnit

@androidx.annotation.RequiresApi(Build.VERSION_CODES.HONEYCOMB)
class DemoApi {
    companion object {

        private const val BASE_URL = BuildConfig.API_BASE_URL
        private val JSON = "application/json".toMediaTypeOrNull()
    }

    private val client = OkHttpClient.Builder()
        .addInterceptor(AddHeaderInterceptor())
        .readTimeout(30, TimeUnit.SECONDS)
        .writeTimeout(40, TimeUnit.SECONDS)
        .connectTimeout(40, TimeUnit.SECONDS)
        .build()

    fun RequestQueue(): ResponseBody? {
        var token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1OTIxNTE2OTMsImV4cCI6MTU5MjIzODA5MywiaWF0IjoxNTkyMTUxNjkzfQ.dNoGdbE0HGmoOgmwcEUxMnMxD74u2RGygBWZaOBuKQw"
        val call = client.newCall(
            Request.Builder()
                .url("http://localhost:5000/api/Invoice")
                .addHeader("Authorization", "$token")
                .addHeader("Content-Type", "application/json")
                .method("Get",null)
                .method("Get", jsonRequestBody {
                    name("DateBegin").value("2020-01-01T17:16:40")
                    name("DateEnd").value("2020-12-31T17:16:40")
                    name("Entity").value("")
                    name("Type").value("FA")
                    name("Reference").value("")
                }).build()
        )
        val response = call.execute()
        if (!response.isSuccessful) {
            throw ApiException("Error calling /username; $response")
        }else{
            Log.e("MY_APP_TAG", response.body?.string())
        }

        return response.body
    }

    private fun jsonRequestBody(body: JsonWriter.() -> Unit): RequestBody {
        val output = StringWriter()
        JsonWriter(output).use { writer ->
            writer.beginObject()
            writer.body()
            writer.endObject()
        }
        return RequestBody.create(DemoApi.JSON, output.toString())
    }
}