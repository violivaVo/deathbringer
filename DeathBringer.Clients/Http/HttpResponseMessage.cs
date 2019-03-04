using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DeathBringer.Clients.Http
{
    /// <summary>
    /// Complex response message for HTTP invokes
    /// </summary>
    /// <typeparam name="TData">Type of parsed data</typeparam>
    public class HttpResponseMessage<TData>
        where TData : class, new()
    {
        /// <summary>
        /// Original HTTP response message
        /// </summary>
        public HttpResponseMessage Response { get; set; }

        /// <summary>
        /// Parsed data
        /// </summary>
        public TData Data { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="response">HTTP response</param>
        public HttpResponseMessage(HttpResponseMessage response)
        {
            //Validazione argomenti
            if (response == null) throw new ArgumentNullException(nameof(response));

            //Imposto il valore
            Response = response;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="response">HTTP response</param>
        /// <param name="data">Parsed response data</param>
        public HttpResponseMessage(HttpResponseMessage response, TData data)
            : this(response)
        {
            //Imposto il valore
            Data = data;
        }

        /// <summary>
        /// Create response for 401 Unauthorized
        /// </summary>
        /// <param name="content">Optional content</param>
        /// <returns>Returns instance of response</returns>
        public static HttpResponseMessage<TData> Unauthorized(string content = "")
        {
            //Creazione della response
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                //Segnalazione del messaggio di errore
                Content = new StringContent(content)
            };

            //Creazione istanza wrapped
            return new HttpResponseMessage<TData>(response);
        }

        /// <summary>
        /// Create response for 404 NotFount
        /// </summary>
        /// <returns>Returns instance of response</returns>
        public static HttpResponseMessage<TData> NotFound()
        {
            //Creazione della response
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            //Creazione istanza wrapped
            return new HttpResponseMessage<TData>(response);
        }

        /// <summary>
        /// Create response for 400 BadRequest
        /// </summary>
        /// <returns>Returns instance of response</returns>
        public static HttpResponseMessage<TData> BadRequest(string content = "")
        {
            //Creazione della response
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                //Segnalazione del messaggio di errore
                Content = new StringContent(content)
            };

            //Creazione istanza wrapped
            return new HttpResponseMessage<TData>(response);
        }
    }
}
