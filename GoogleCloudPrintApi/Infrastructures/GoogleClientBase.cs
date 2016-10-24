﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleCloudPrintApi.Models;

namespace GoogleCloudPrintApi.Infrastructures
{
    public abstract class GoogleClientBase
    {
        public GoogleClientBase(GoogleOAuth2ProviderBase oAuth2Provider, Token token)
        {
            if (oAuth2Provider == null)
                throw new ArgumentNullException(nameof(oAuth2Provider));
            _oAuth2Provider = oAuth2Provider;

            if (token == null || token.RefreshToken == null)
                throw new ArgumentNullException(nameof(token));
            _token = token;
        }

        protected GoogleOAuth2ProviderBase _oAuth2Provider;

        protected Token _token;

        /// <summary>
        /// Update access token if it is expired
        /// </summary>
        /// <returns>Access token</returns>
        protected async Task UpdateToken()
        {
            if (_token.ExpireDateTime > DateTime.Now)
                return;

            _token = await _oAuth2Provider.GenerateAccessTokenAsync(_token.RefreshToken);
        }
    }
}