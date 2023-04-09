// See https://aka.ms/new-console-template for more information
using DemoDynamicProto.Helper;
using DynamicGrpc;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.Reflection;
using Grpc.Net.Client;
using System.IO;
using System.Reflection;

Console.WriteLine("Read Proto Dynamic Schema");

// Fetch reflection data from server
var httpClientHandler = new HttpClientHandler();
// Return `true` to allow certificates that are untrusted/invalid
httpClientHandler.ServerCertificateCustomValidationCallback =
    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

var httpClient = new HttpClient(httpClientHandler);

var channel = GrpcChannel.ForAddress("https://localhost:7236", new GrpcChannelOptions
{
    HttpClient = httpClient
});


var client = await DynamicGrpcClient.FromServerReflection(channel);

// Call the method `Handle` on the service `SpaceX.API.Device.Device`
string str = "{\"name\":\"Vilakhan\",\"surname\":\"Saykhambay\",\"age\":10,\"address\":{\"village\":\"Dongdok\",\"district\":\"Saythany\",\"province\":\"Vientiane\"}}";
var contentObj = (Dictionary<string, object>)JsonHelper.Deserialize(str);
//var content = new Dictionary<string, object>()
//{
//    { "name", "Kommaly" },
//    { "surname", "Saykhambay" },
//    { "age", 32 },
//    { "address",
//        new Dictionary<string, object>()
//        {
//            { "village", "Dongdok" },
//            { "district", "Xaythany" },
//            { "province", "Vientiane" }
//        }
//    }
//};
//var contentJson = System.Text.Json.JsonSerializer.Serialize(content);
//var result = await client.AsyncUnaryCall("greet.Greeter", "SayHello", contentObj);

var content = new Dictionary<string, object>()
{
};

var result = await client.AsyncUnaryCall("dynamicmenumicroservice.DynamicMenuMicroservice", "GetMenuProfile", content);


// Print a proto descriptor
FileDescriptor descriptor = client.Files[0];
Console.WriteLine(descriptor.ToProtoString());

Console.WriteLine("--------------------------------------------------");
Console.WriteLine($"Response: {System.Text.Json.JsonSerializer.Serialize(result)}");


