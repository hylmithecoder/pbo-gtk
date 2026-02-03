using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace pbo.Pages;

#region Response Handler
public class InventoryResponse 
{
    public string status { get; set; }
    public List<InventoryModel>? data { get; set; }
}

public class InventoryModel 
{
    public int? id { get; set; }
    public string? created_at { get; set; }
    public string? name { get; set; }
    public int? stock { get; set; }
    public double? price { get; set; }
    public string? created_by { get; set; }
}
#endregion

public class ServiceHandler
{
    public const string BaseUrl = "http://localhost/smart_inventory_solution/";
    private readonly HttpClient _httpClient;

    public ServiceHandler()
    {
        _httpClient = new HttpClient();
    }
    
    public async Task<InventoryResponse> GetInventory()
    {
        try
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            var data = await response.Content.ReadFromJsonAsync<InventoryResponse>();
            Console.WriteLine(data.status);
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}