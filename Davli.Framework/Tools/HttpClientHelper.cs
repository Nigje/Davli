using Newtonsoft.Json;
using Davli.Framework.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Davli.Framework.Tools
{
    /// <summary>
    /// Http Helper
    /// </summary>
    public class HttpClientHelper
    {
        //*******************************************************************************************************
        //Variables:

        //*******************************************************************************************************
        //Todo: ensure the exception has handled.
        private static async Task ValidateResponse(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
                return;
            throw new DavliException("An error has occurred.", await httpResponseMessage.Content.ReadAsStringAsync());
        }

        //*******************************************************************************************************
        /// <summary>
        /// Post
        /// </summary>
        /// <typeparam name="Tinput"></typeparam>
        /// <param name="httpClinet"></param>
        /// <param name="url"></param>
        /// <param name="inputObject"></param>
        /// <returns></returns>
        public static async Task PostAsync<Tinput>(HttpClient httpClinet, string url, Tinput inputObject)
        {
            var content = new StringContent(JsonConvert.SerializeObject(inputObject), Encoding.UTF8, "application/json");
            var result = await httpClinet.PostAsync(url, content);
            await ValidateResponse(result);
        }
        //*******************************************************************************************************
        /// <summary>
        /// Post
        /// </summary>
        /// <typeparam name="Tinput"></typeparam>
        /// <param name="url"></param>
        /// <param name="inputObject"></param>
        /// <returns></returns>
        public static async Task PostAsync<Tinput>(string url, Tinput inputObject)
        {
            await Post<Tinput>(new HttpClient(), url, inputObject);
        }
        //*******************************************************************************************************
        /// <summary>
        /// Post
        /// </summary>
        /// <param name="httpClinet"></param>
        /// <param name="url"></param>
        /// <param name="contentValue"></param>
        /// <typeparam name="TOutPut"></typeparam>
        /// <returns></returns>
        public static async Task<TOutPut> Post<TOutPut>(HttpClient httpClinet, string url, object contentValue)
        {
            return await PostAsync<object, TOutPut>(httpClinet, url, contentValue);
        }

        //*******************************************************************************************************
        /// <summary>
        /// Post
        /// </summary>
        /// <typeparam name="Tinput"></typeparam>
        /// <typeparam name="TOutPut"></typeparam>
        /// <param name="httpClinet"></param>
        /// <param name="url"></param>
        /// <param name="contentValue"></param>
        /// <returns></returns>
        public static async Task<TOutPut> PostAsync<Tinput, TOutPut>(HttpClient httpClinet, string url, Tinput contentValue)
        {
            var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json");
            var response = await httpClinet.PostAsync(url, content);
            await ValidateResponse(response);
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TOutPut>(result);
        }
        //*******************************************************************************************************
        /// <summary>
        /// Post.
        /// </summary>
        /// <typeparam name="Tinput"></typeparam>
        /// <typeparam name="TOutPut"></typeparam>
        /// <param name="url"></param>
        /// <param name="contentValue"></param>
        /// <returns></returns>
        public static async Task<TOutPut> PostAsync<Tinput, TOutPut>(string url, Tinput contentValue)
        {
            return await PostAsync<Tinput, TOutPut>(new HttpClient(), url, contentValue);
        }
        //*******************************************************************************************************
        public static async Task<TOutPut> Post<TOutPut>(string url, object contentValue)
        {
            return await PostAsync<object, TOutPut>(new HttpClient(), url, contentValue);
        }
        //*******************************************************************************************************
        /// <summary>
        /// Put
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClinet"></param>
        /// <param name="url"></param>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static async Task Put<T>(HttpClient httpClinet, string url, T stringValue)
        {
            var content = new StringContent(JsonConvert.SerializeObject(stringValue), Encoding.UTF8, "application/json");
            var result = await httpClinet.PutAsync(url, content);
            await ValidateResponse(result);
        }

        //*******************************************************************************************************
        /// <summary>
        /// Put
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="stringValue"></param>
        /// <returns></returns>
        public static async Task Put<T>(string url, T stringValue)
        {
            await Put<T>(new HttpClient(), url, stringValue);
        }
        //*******************************************************************************************************
        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClinet"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<T> Get<T>(HttpClient httpClinet, string url)
        {
            var result = await httpClinet.GetAsync(url);
            await ValidateResponse(result);
            string resultContentString = await result.Content.ReadAsStringAsync();
            if (typeof(T) == typeof(string)) return (T)(object)resultContentString;
            T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
            return resultContent;
        }
        //*******************************************************************************************************
        /// <summary>
        /// Get byte[]
        /// </summary>
        /// <param name="httpClinet"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetByteArrayAsync(HttpClient httpClinet, string url)
        {
            var result = await httpClinet.GetAsync(url);
            await ValidateResponse(result);
            return await result.Content.ReadAsByteArrayAsync();
        }
        //*******************************************************************************************************
        /// <summary>
        /// Get byte[]
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetByteArrayAsync(string url)
        {
            return await GetByteArrayAsync(new HttpClient(), url);
        }
        //*******************************************************************************************************
        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClinet"></param>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static async Task<T> Get<T>(HttpClient httpClinet, string url, object obj)
        {
            return await Get<T>(httpClinet, url + "?" + obj.ToQueryString());
        }

        //*******************************************************************************************************
        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<T> Get<T>(string url)
        {
            return await Get<T>(new HttpClient(), url);
        }
        //*******************************************************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static async Task<T> Get<T>(string url, object obj)
        {
            return await Get<T>(new HttpClient(), url + "?" + obj.ToQueryString());
        }
        //*******************************************************************************************************
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="httpClinet"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task DeleteAsync(HttpClient httpClinet, string url)
        {
            var result = await httpClinet.DeleteAsync(url);
            await ValidateResponse(result);
        }
        //*******************************************************************************************************
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task DeleteAsync(string url)
        {
            await DeleteAsync(new HttpClient(), url);
        }
        //*******************************************************************************************************
    }
}
