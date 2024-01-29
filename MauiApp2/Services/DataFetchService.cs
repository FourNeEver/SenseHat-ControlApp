using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MauiApp2;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Maui.Layouts;

namespace MauiApp2.Services
{
    public static class DataFetchService
    {
        static string Url = "http://192.168.1.74/Mock/resource.php?id=0001";

        static HttpClient client;
        static Uri Adress = new Uri(Url);
        public static bool Connection = false;
        static DataFetchService() 
        {
            client = new HttpClient()
            {
                Timeout = System.TimeSpan.FromSeconds(1)
            };   
        }

        public static void Connected()
        {
            Connection = true;
        }

        public static void Disconnected()
        {
            Connection = false;
        }
        
        public static void Disconnect()
        {
            client.CancelPendingRequests();
            
        }

        public static async Task TestConnection()
        {
            string json = null;
            try { json = await client.GetStringAsync(Adress + "/getAllData"); }
            catch (TimeoutException)
            {
                Disconnected();
                
            }
            catch (TaskCanceledException)
            {
                Disconnected();
                
            }
            catch (IOException)
            {
                Disconnected();
            }
            catch (SocketException)
            {
                Disconnected();
            }
            catch (HttpRequestException)
            {
                Disconnected();
            }
            Connected();
        }

        public static async Task<string> GetData(Uri adress)
        {
            Adress = adress;
            string json = null;
            try { json = await client.GetStringAsync(Adress+ "/getEnvironmentData");
                //await client.GetAsync(Adress + "/getAllData");
            }
            catch(TimeoutException)
            {
                Disconnected();
                return null;
            }
            catch (TaskCanceledException)
            {
                Disconnected();
                return null;
            }
            catch (IOException)
            {
                return null;
            }
            catch (SocketException)
            {
                return null;
            }
            //json.ToString();
            return json;
        }

        public static async Task<string> GetData()
        {
            string json = null;
            try { json = await client.GetStringAsync(Adress+ "/getAllData"); }
            catch (TimeoutException)
            {
                Disconnected();
                return null;
            }
            catch (TaskCanceledException)
            {
                Disconnected();
                return null;
            }
            catch (IOException)
            {
                return null;
            }
            catch (SocketException)
            {
                return null;
            }
            return json;
        }

        public static async Task PostLedData(PixelData data)
        {
            var json = JsonSerializer.Serialize(data);
            try {
                //await client.PostAsJsonAsync(Adress + "/setOnePixel", data);
                await client.PostAsync(Adress + "/setOnePixel", new StringContent(json, Encoding.UTF8,"application/json"));
                //await client.PostAsJsonAsync(Adress + "/setOnePixel", "{\"x\":0,  \"y\":0, \"R\":134, \"G\":145,\"B\":12}");

            }
            catch (TimeoutException)
            {
                Disconnected();
            }
            catch (TaskCanceledException)
            {
                Disconnected();
            }

        }


        public static async Task PostLedData()
        {
            try
            {
                //await client.PostAsJsonAsync(Adress + "/setOnePixel", data);
                await client.PostAsync(Adress + "/clearScreen", new StringContent("0", Encoding.UTF8, "application/json"));
                //await client.PostAsJsonAsync(Adress + "/setOnePixel", "{\"x\":0,  \"y\":0, \"R\":134, \"G\":145,\"B\":12}");

            }
            catch (TimeoutException)
            {
                Disconnected();
            }
            catch (TaskCanceledException)
            {
                Disconnected();
            }

        }


        public static async Task<string> GetLedData()
        {
            string json = null;
            try { json = await client.GetStringAsync(Adress + "/getPixels"); }
            catch (TimeoutException)
            {
                Disconnected();
                return null;
            }
            catch (TaskCanceledException)
            {
                Disconnected();
                return null;
            }
            catch (IOException)
            {
                return null;
            }
            catch (SocketException)
            {
                return null;
            }
            return json;
        }
    }
}
