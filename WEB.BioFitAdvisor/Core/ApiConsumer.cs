using WEB.BioFitAdvisor.Models;
using System.Text;

namespace WEB.BioFitAdvisor.Core
{
    public class ApiConsumer
    {
        private string _BaseURL;

        public ApiConsumer(IConfiguration configuration)
        {
            _BaseURL = configuration["UrlBaseApi"] ?? "";
        }

        public async Task<apiDTO> GET(string ResourseURL, string Token, Dictionary<string, string> parameters = null)
        {
            apiDTO dto = new apiDTO();

            try
			{
                string data = "";

                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri(_BaseURL);

                    string urlWithParams = ResourseURL;

                    if (parameters != null && parameters.Count > 0)
                    {
                        var queryString = string.Join("&", parameters.Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));
                        urlWithParams += "?" + queryString;
                    }

                    if (Token != "")
                    {
                        client.DefaultRequestHeaders.Add("AccessToken", "Bearer " + Token);
                    }

                    var response = await client.GetAsync(_BaseURL + urlWithParams);

                    if (response.IsSuccessStatusCode)
                    {
                        data = await response.Content.ReadAsStringAsync();

                        dto.success = true;
                        dto.response = data;
                    }
                }

                return dto;
            }
			catch (Exception)
			{
                return dto;
			}
        }

        public async Task<apiDTO> POST(string ResourseURL, string Token, string bodyJSON, Dictionary<string, string> parameters = null)
        {
            apiDTO dto = new apiDTO();

            try
            {
                string data = "";

                using (var client = new HttpClient())
                {
                    
                    //client.BaseAddress = new Uri(_BaseURL);

                    string urlWithParams = ResourseURL;

                    if (parameters != null && parameters.Count > 0)
                    {
                        var queryString = string.Join("&", parameters.Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));
                        urlWithParams += "?" + queryString;
                    }

                    if (Token != "")
                    {
                        client.DefaultRequestHeaders.Add("AccessToken", "Bearer " + Token);
                    }

                    var content = new StringContent(bodyJSON, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(_BaseURL + urlWithParams, content);

                    if (response.IsSuccessStatusCode)
                    {
                        data = await response.Content.ReadAsStringAsync();
                        
                        dto.success = true;
                        dto.response = data;
                    }
                }

                return dto;
            }
            catch (Exception)
            {
                return dto;
            }
        }

        public async Task<apiDTO> PUT(string ResourseURL, string Token, string jsonData, Dictionary<string, string> parameters = null)
        {
            apiDTO dto = new apiDTO();

            try
            {
                string data = "";

                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri(_BaseURL);

                    string urlWithParams = ResourseURL;

                    if (parameters != null && parameters.Count > 0)
                    {
                        var queryString = string.Join("&", parameters.Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));
                        urlWithParams += "?" + queryString;
                    }

                    if (Token != "")
                    {
                        client.DefaultRequestHeaders.Add("AccessToken", "Bearer " + Token);
                    }

                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(_BaseURL + urlWithParams, content);

                    if (response.IsSuccessStatusCode)
                    {
                        dto.success = true;
                        dto.response = data;
                    }
                }

                return dto;
            }
            catch (Exception)
            {
                return dto;
            }
        }

        public async Task<apiDTO> DELETE(string ResourseURL, string Token, Dictionary<string, string> parameters = null)
        {
            apiDTO dto = new apiDTO();

            try
            {
                string data = "";

                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri(_BaseURL);

                    string urlWithParams = ResourseURL;

                    if (parameters != null && parameters.Count > 0)
                    {
                        var queryString = string.Join("&", parameters.Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));
                        urlWithParams += "?" + queryString;
                    }

                    if (Token != "")
                    {
                        client.DefaultRequestHeaders.Add("AccessToken", "Bearer " + Token);
                    }

                    var response = await client.DeleteAsync(_BaseURL + urlWithParams);

                    if (response.IsSuccessStatusCode)
                    {
                        dto.success = true;
                        dto.response = data;
                    }
                }

                return dto;
            }
            catch (Exception)
            {
                return dto;
            }
        }
    }
}
