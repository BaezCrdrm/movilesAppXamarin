using MovilesApp.Model;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Diagnostics;
using Windows.Data.Json;
using System.IO;

namespace MovilesApp.Logic
{
    public class DataAsync
    {
        public async Task<string> Get(Event ev)
        {
            HttpStatusCode response;
            string result = "";

            try
            {
                string strUrl = String.Format("http://livebr.esy.es/scripts/service/rest/req_events.php?evId={0}&tpId=",
                    ev.Id);
                if (ev.Type.Id > 0)
                    strUrl += ev.Type.Id.ToString();

                HttpClientHandler handler = new HttpClientHandler();
                HttpClient con = new HttpClient(handler);
                var resp = await con.GetAsync(strUrl);
                Debug.WriteLine(resp.ToString());
                
                try
                {                   
                    response = resp.StatusCode;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Ha habido un error al establecer la conexión");
                    Debug.WriteLine(ex.Message);
                    return null;
                }

                if (response == HttpStatusCode.OK)
                {
                    result = await resp.Content.ReadAsStringAsync();
                    return result;

                    /*string json = "";
                    var r = await resp.Content.ReadAsStreamAsync();
                    using (StreamReader tr = new StreamReader(r))
                    {
                        json = tr.ReadToEnd();
                    }
                    return json;*/
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}
