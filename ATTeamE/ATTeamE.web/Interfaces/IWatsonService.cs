using ATTeamE.web.Domain.Convo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATTeamE.web.Interfaces
{
    public interface IWatsonService
    {
        byte[] ConvertTextToSpeech(string text);
        Message GetResponseMessage(string request);
        string GetToken(string apiName);
    }
}