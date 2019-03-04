using DeathBringer.Clients.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DeathBringer.Clients.Clients.Common
{
    /// <summary>
    /// Asbtract service base class for HTTP provider
    /// </summary>
    public abstract class HttpClientBase : IDisposable
    {
        #region Private fields
        private bool _IsDisposed;
        private readonly HttpClient _Client;
        #endregion

        /// <summary>
        /// Client base address
        /// </summary>
        public Uri BaseAddress => _Client?.BaseAddress;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseUrl">Base URL for API service</param>
        protected HttpClientBase(string baseUrl)
        {
            //Validazione argomenti
            if (string.IsNullOrEmpty(baseUrl)) throw new ArgumentNullException(nameof(baseUrl));

            //Inizializzo il client
            _Client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            //Imposto i default
            _Client.DefaultRequestHeaders.Accept.Clear();
            _Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Execute a post on remove server
        /// </summary>
        /// <typeparam name="TRequest">Type of request</typeparam>
        /// <typeparam name="TResponse">Type or response</typeparam>
        /// <param name="partialUrl">Partial URL</param>
        /// <param name="method">HTTP method</param>
        /// <param name="request">Request</param>
        /// <param name="authentication">Authentication header</param>
        /// <returns>Returns task with response</returns>
        public async Task<HttpResponseMessage<TResponse>> Invoke<TRequest, TResponse>(string partialUrl,
            HttpMethod method, TRequest request = null, AuthenticationHeaderValue authentication = null)
            where TRequest : class, new()
            where TResponse : class, new()
        {
            //Validazione argomenti
            if (string.IsNullOrEmpty(partialUrl)) throw new ArgumentNullException(nameof(partialUrl));

            //Creo il messaggio di request con l'url e il verb
            HttpRequestMessage message = new HttpRequestMessage(method, partialUrl);

            //Aggiungo l'header "Accept"
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Serializzo in formato JSON e imposto il contenuto (se diverso da null)
            if (request != null)
                message.Content = new StringContent(
                    JsonConvert.SerializeObject(request),
                    Encoding.UTF8, "application/json");

            //Se ho un token autorizzativo
            if (authentication != null)
                message.Headers.Authorization = authentication;

            try
            {
                //Eseguo la chiamata del client
                var response = await _Client.SendAsync(message);

                //Eseguo la creazione della response
                HttpResponseMessage<TResponse> contractResponse = new HttpResponseMessage<TResponse>(response);

                //Se la chiamata ha avuto successo (200 - Ok)
                if (!response.IsSuccessStatusCode)
                    return contractResponse;

                //Recupero il contento presente nella response
                var jsonContent = await response.Content.ReadAsStringAsync();

                try
                {
                    //In alcuni casi il JSON di risposta potrebbe contentere dei caratteri di escape
                    //che impediscono la corretta deserializzazione del dato; è il caso di Trouble Ticketing
                    //che per il metodo "FetchCustomerEmployees" contiene dei caratteri "\" che possono essere
                    //deserializzati solo se vengono trasformati in "\\". Per evitare problematiche eseguo
                    //la trasformazione SOLO se ci sono errore di serializzazione in prima battuta. Questo
                    //perchè alcune situazioni tipo "\n" sono ammesse in JSON e rappresentano una corretta
                    //codifica che applicando "a tappeto" il raddoppio degli escape si perderebbe
                    if (!string.IsNullOrEmpty(jsonContent))
                    {
                        //Deserializzo solo se ho un contenuto valido
                        contractResponse.Data = JsonConvert.DeserializeObject<TResponse>(jsonContent);
                    }
                }
                catch (Exception jsonException)
                {
                    //Deserializzazione solo con contenuto valido
                    if (!string.IsNullOrEmpty(jsonContent))
                    {
                        //Tento il raddoppio degli escape
                        jsonContent = jsonContent.Replace(@"\", @"\\");

                        //Deserializzo solo se ho un contenuto valido
                        contractResponse.Data = JsonConvert.DeserializeObject<TResponse>(jsonContent);
                    }
                }

                //Ritorno la response
                return contractResponse;

                //Ritorno semplicemente la response
            }
            catch (Exception exc)
            {
                //Si potrebbe verificare l'irraggiungibilità dell'host remoto
                //perchè magari non disponibile. In questi casi è buona regola
                //gestire la questione con un "ServiceAnavailable" impostando nel body
                //della response l'eccezione generata
                HttpResponseMessage serviceAnavailableResponse =
                    new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                    {
                        Content = new StringContent(exc.ToString())
                    };
                return new HttpResponseMessage<TResponse>(serviceAnavailableResponse);
            }
        }

        /// <summary>
        /// Executes a GET over HTTP
        /// </summary>
        /// <param name="partialUrl">Partial URL</param>
        /// <returns>Returns async task</returns>
        public async Task<HttpResponseMessage> Get(string partialUrl)
        {
            //Validazione argomenti
            if (string.IsNullOrEmpty(partialUrl)) throw new ArgumentNullException(nameof(partialUrl));

            //Creo il messaggio di request con l'url e il verb
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, partialUrl);

            //Eseguo la chiamata del client
            var response = await _Client.SendAsync(message);

            //Ritorno il task
            return response;
        }

        /// <summary>
        /// Finalizer that ensures the object is correctly disposed of.
        /// </summary>
        ~HttpClientBase()
        {
            //Richiamo i dispose implicito
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, 
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            //Eseguo una dispose esplicita
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, 
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="isDisposing">Explicit dispose</param>
        protected virtual void Dispose(bool isDisposing)
        {
            //Se l'oggetto è già rilasciato, esco
            if (_IsDisposed)
                return;

            //Se è richiesto il rilascio esplicito
            if (!isDisposing)
            {
                //RIlascio della logica non finalizzabile
                _Client.Dispose();
            }

            //Marco il dispose e invoco il GC
            _IsDisposed = true;
        }
    }
}
