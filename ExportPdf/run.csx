using System.Net;
using System.Net.Http.Headers;
using OpenHtmlToPdf;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("Processing PDF Request");

    string html = await req.Content.ReadAsStringAsync();

    var pdf = Pdf
        .From(html)
        .Content();

    log.Info($"PDF Generated. Length={pdf.Length}");

    var res = new HttpResponseMessage(HttpStatusCode.OK);
        res.Content = new ByteArrayContent(pdf);
        res.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
        res.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");

    return res;
}