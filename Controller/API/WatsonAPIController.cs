using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ATTeamE.web.Interfaces;

namespace ATTeamE.web.Controllers.API
{
    public class WatsonApiController : ApiController
    {
        private IWatsonService _watsonService;

        public WatsonApiController(IWatsonService WatsonService)
        {
            _watsonService = WatsonService;
        }
        // GET api/<controller>
        [System.Web.Http.Route("api/[controller]")]
        public HttpResponseMessage GetSpeech()
        {

            try
            {
               // var results = _watsonService.ConvertTextToSpeech();
               // string url = "https://watson-api-explorer.mybluemix.net/text-to-speech/api/v1/synthesize?accept=audio%2Fogg%3Bcodecs%3Dopus&voice=en-US_MichaelVoice&text=" + results;
                
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK);//, response);
        }


        //GET api/<controller>
        public void ConvertSpeechToText() 
        {
            try
            {
                // var results = _watsonService.ConvertSpeechToText();
                // string url = "https://watson-api-explorer.mybluemix.net/text-to-speech/api/v1/synthesize?" + 

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
