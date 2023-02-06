using ConsumeWsdlExample.Xmls;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace ConsumeWsdlExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ConsultarCEP();

            return View();
        }

        public HttpWebRequest CreateWebRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(@"https://apps.correios.com.br/SigepMasterJPA/AtendeClienteService/AtendeCliente?wsdl");

            webRequest.Headers.Add(@"SOAP:Action");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }


        private void ConsultarCEP()
        {
            var cep = new Cep
            {
                Name = "cep",
                TextContent = "54280-298"
            };

            var consultaCep = new ConsultaCep {
                Name = "consultaCEP",
                Children = new List<Xmls.XmlElement> { cep }
            };

            var header = new Header
            {
                Name = nameof(Header)
            };
            var body = new BodyRequest
            {
                Name = "Body",
                Children = new List<Xmls.XmlElement> { consultaCep }
            };
            var envelope = new EnvelopeRequest
            {
                Name = "Envelope",
                Children = new List<Xmls.XmlElement> { header, body }
            };

            var xmlRequest = new XmlGenerator().GenerateToString(envelope);


            HttpWebRequest request = CreateWebRequest();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            soapEnvelopeXml.LoadXml(xmlRequest);


            using (Stream stream = request.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(soapResult);

                    XmlSerializer serializer = new XmlSerializer(typeof(Envelope));
                    StringReader rdr = new StringReader(soapResult);
                    Envelope resultingMessage = (Envelope) serializer.Deserialize(rdr);
                }
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    [XmlElementPrefix("soapenv", Namespace = "http://schemas.xmlsoap.org/soap/envelope/", SetNamespace = true)]
    [XmlElementAttr("cli", "http://cliente.bean.master.sigep.bsb.correios.com.br/")]
    public class EnvelopeRequest : Xmls.XmlElement { }

    [XmlElementPrefix("soapenv", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Header : Xmls.XmlElement { }

    [XmlElementPrefix("soapenv", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class BodyRequest : Xmls.XmlElement { }

    [XmlElementPrefix("cli", Namespace = "http://cliente.bean.master.sigep.bsb.correios.com.br/")]
    public class ConsultaCep : Xmls.XmlElement { }

    public class Cep : Xmls.XmlElement { }
}