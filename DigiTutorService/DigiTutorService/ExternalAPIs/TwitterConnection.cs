using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tweetinvi;
using Tweetinvi.Models;

namespace DigiTutorService.ExternalAPIs
{
    public static class TwitterConnection
    {
        // Generate credentials that we want to use
        static TwitterCredentials creds = new TwitterCredentials("G7aCvHb5Hufk3YcjjHKawNub0", "oKiHAA76OmcNeaGKtR22pp5TrObWUXNLI49qj7RUdgT2Cv3rET", "931973066627993600-Ke3cXcyJuq5FoWh7R8djNbdfesXtxYB", "VO0fswBj43tDZ2AFwooRB6hEC3SSSaHj1bARmukPWpfZF");

        public static void sendTweet(string usuario1, string usuario2, string habilidad)
        {
            // Use the ExecuteOperationWithCredentials method to invoke an operation with a specific set of credentials
            var tweet = Auth.ExecuteOperationWithCredentials(creds, () =>
            {
                return Tweet.PublishTweet(string.Format(
                    "{0} ha dado un nuevo apoyo a {1} en la habilidad {2}", 
                    usuario1, 
                    usuario2, 
                    habilidad));
            });
        }
    }
}