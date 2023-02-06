//

/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace ConsumeWsdlExample.Controllers
{
    [XmlRoot(ElementName = "return")]
    public class Return
    {
        [XmlElement(ElementName = "bairro")]
        public string Bairro { get; set; }

        [XmlElement(ElementName = "cep")]
        public string Cep { get; set; }
        [XmlElement(ElementName = "cidade")]
        public string Cidade { get; set; }
        [XmlElement(ElementName = "complemento2")]
        public string Complemento2 { get; set; }
        [XmlElement(ElementName = "end")]
        public string End { get; set; }
        [XmlElement(ElementName = "uf")]
        public string Uf { get; set; }
    }

    [XmlRoot(ElementName = "consultaCEPResponse", Namespace = "http://cliente.bean.master.sigep.bsb.correios.com.br/")]
    public class ConsultaCEPResponse
    {
        [XmlElement(ElementName = "return", Namespace = "")]
        public Return Return { get; set; }
    }

    [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Body
    {
        [XmlElement(ElementName = "consultaCEPResponse", Namespace = "http://cliente.bean.master.sigep.bsb.correios.com.br/")]
        public ConsultaCEPResponse ConsultaCEPResponse { get; set; }
    }

    [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class Envelope
    {
        [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public Body Body { get; set; }
        [XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Soap { get; set; }
    }

}
