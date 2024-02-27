using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf.Compiler;
using Newtonsoft.Json;
using Utilis.ObjectModel;

namespace MagicNumber.Core.Model
{
    public class AzureServer : BaseNotifyPropertyChanged, IServer
    {
        private HttpClient _httpClient;

        public string Name { get; }
        private Set [] m_sets;
        public Set [] Sets
        {
            get { return m_sets; }
            private set { SetProperty ( ref m_sets, value ); }
        }

        public AzureServer(string name)
        {
            _httpClient = new HttpClient ( );
            _httpClient.BaseAddress = new Uri ( name );
        }

        public void Load ( )
        {
            var response = _httpClient.GetAsync ( "api/v1/GetSets" ).Result;
            if ( response.IsSuccessStatusCode )
            {
                string content = response.Content.ReadAsStringAsync ( ).Result;
                Sets = JsonConvert.DeserializeObject<Set []> ( content ) ?? throw new Exception("Unable to parse JSON");
            }
            else
            {
                throw new Exception ( "Unable to load sets! (Code: " + response.StatusCode + ")" );
            }
        }

        public void Add ( Set s, int initialBlock )
        {
            var payload = new
            {
                s,
                initialBlock
            };

            string jsonPayload = JsonConvert.SerializeObject ( payload );
            var content = new StringContent ( jsonPayload, Encoding.UTF8, "application/json" );

            var response = _httpClient.PostAsync ( "api/v1/AddSet", content ).Result;
            if ( response.IsSuccessStatusCode )
            {
                string responseContent = response.Content.ReadAsStringAsync ( ).Result;
                Sets = JsonConvert.DeserializeObject<Set []> ( responseContent ) ?? throw new Exception ( "Unable to parse JSON" );
            }
            else
            {
                throw new Exception ( "Unable to add set! (Code: " + response.StatusCode + ")" );
            }
        }

        public void Remove ( Set s )
        {
            var payload = new
            {
                s
            };
            string jsonPayload = JsonConvert.SerializeObject ( payload );
            var content = new StringContent ( jsonPayload, Encoding.UTF8, "application/json" );

            var response = _httpClient.PostAsync ( "api/v1/RemoveSet", content ).Result;
            if ( response.IsSuccessStatusCode )
            {
                string responseContent = response.Content.ReadAsStringAsync ( ).Result;
                Sets = JsonConvert.DeserializeObject<Set []> ( responseContent ) ?? throw new Exception ( "Unable to parse JSON" );
            }
            else
            {
                throw new Exception ( "Unable to remove set! (Code: " + response.StatusCode + ")" );
            }
        }

        public int GetNextBlock ( Set s )
        {
            var response = _httpClient.GetAsync ( "api/v1/GetNextBlock?setId=" + s.Id ).Result;
            if ( response.IsSuccessStatusCode )
            {
                string responseContent = response.Content.ReadAsStringAsync ( ).Result;
                return int.Parse ( responseContent );
            }
            else
            {
                throw new Exception ( "Unable get next block! (Code: " + response.StatusCode + ")" );
            }
        }
    }
}
