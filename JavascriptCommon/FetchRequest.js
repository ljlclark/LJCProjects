// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// FetchRequest.js

// Represents an HttpRequest.
class FetchRequest
{
  // The Constructor function.
  constructor()
  {
  }

  // Get data with fetch().
  Fetch(sUrl, cFunction)
  {
    fetch(sUrl)
      .then(function (response)
      {
        if (response.ok)
        {
          //return response.json();
          return response.text();
        }
        else
        {
          return Promise.reject({
            status: response.status,
            statusText: response.statusText
          });
        }
      })
      .then(function (data)
      {
        cFunction(data);
      })
      .catch(function (error)
      {
        alert(error);
      });
  }
}