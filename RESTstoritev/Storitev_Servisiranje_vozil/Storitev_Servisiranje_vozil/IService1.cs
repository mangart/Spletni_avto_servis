using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Storitev_Servisiranje_vozil
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);


        [OperationContract]
        [WebGet(UriTemplate = "BlaBla", ResponseFormat = WebMessageFormat.Json)]
        string Blabla();

        [OperationContract]
        [WebInvoke(UriTemplate = "Registracija", ResponseFormat = WebMessageFormat.Json)]
        int Registriraj(Regist oseba);

        [OperationContract]
        [WebGet(UriTemplate = "Get_oseba", ResponseFormat = WebMessageFormat.Json)]
        Oseba get_oseba();

        [OperationContract]
        [WebGet(UriTemplate = "Get_znamke", ResponseFormat = WebMessageFormat.Json)]
        List<Vozilo> get_znamke();

        [OperationContract]
        [WebGet(UriTemplate = "Get_modeli/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<Vozilo> get_modeli(string id);

        [OperationContract]
        [WebGet(UriTemplate = "Get_poslovalnice", ResponseFormat = WebMessageFormat.Json)]
        List<Poslovalnica> get_poslovalnice();

        [OperationContract]
        [WebGet(UriTemplate = "Get_vozila/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<Vozilo> get_vozila(string id);

        [OperationContract]
        [WebGet(UriTemplate = "Get_neodobrena_narocila/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<Narocilo> get_neodobrena_narocila(string id);

        [OperationContract]
        [WebGet(UriTemplate = "Get_odobrena_narocila/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<Narocilo> get_odobrena_narocila(string id);

        [OperationContract]
        [WebGet(UriTemplate = "Get_opravljena_narocila/{id}", ResponseFormat = WebMessageFormat.Json)]
        List<Narocilo> get_opravljena_narocila(string id);

        [OperationContract]
        [WebGet(UriTemplate = "Get_neodobrena_narocila_poslovalnice/{idp}", ResponseFormat = WebMessageFormat.Json)]
        List<Narocilo> get_neodobrena_narocila_poslovalnice(string idp);

        [OperationContract]
        [WebGet(UriTemplate = "Get_odobrena_narocila_poslovalnice/{idp}", ResponseFormat = WebMessageFormat.Json)]
        List<Narocilo> get_odobrena_narocila_poslovalnice(string idp);

        [OperationContract]
        [WebInvoke(UriTemplate = "Odobri_narocila/{idn}", ResponseFormat = WebMessageFormat.Json, Method = "PUT")]
        string Odobri_narocila(string idn);

        [OperationContract]
        [WebInvoke(UriTemplate = "Opravi_narocila/{idn}", ResponseFormat = WebMessageFormat.Json, Method = "PUT")]
        string Opravi_narocila(string idn);

        [OperationContract]
        [WebInvoke(UriTemplate = "Izbrisi_narocilo/{idn}", ResponseFormat = WebMessageFormat.Json, Method = "DELETE")]
        string Izbrisi_narocilo(string idn);

        [OperationContract]
        [WebInvoke(UriTemplate = "Izbrisi_vozilo/{idv}", ResponseFormat = WebMessageFormat.Json, Method = "DELETE")]
        string Izbrisi_vozilo(string idv);

        [OperationContract]
        [WebInvoke(UriTemplate = "Dodaj_vozilo", ResponseFormat = WebMessageFormat.Json)]
        string Dodaj_vozilo(Vozilo voz);

        [OperationContract]
        [WebInvoke(UriTemplate = "Naroci", ResponseFormat = WebMessageFormat.Json)]
        string Naroci(Narocilo naroc);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
    [DataContract]
    public class Vozilo
    {
        [DataMember]
        public int id_vozila { get; set; }
        [DataMember]
        public int letnica { get; set; }
        [DataMember]
        public string znamka { get; set; }
        [DataMember]
        public string model { get; set; }
        [DataMember]
        public int id_znamke { get; set; }
        [DataMember]
        public int id_modela { get; set; }

    }

    [DataContract]
    public class Poslovalnica
    {
        [DataMember]
        public int id_poslovalnice { get; set; }
        [DataMember]
        public string naziv { get; set; }
        [DataMember]
        public string naslov { get; set; }
        [DataMember]
        public string kraj { get; set; }
    }

    [DataContract]
    public class Narocilo
    {
        [DataMember]
        public int id_narocila { get; set; }
        [DataMember]
        public int id_poslovalnice { get; set; }
        [DataMember]
        public int id_vozila { get; set; }
        [DataMember]
        public int id_znamke { get; set; }
        [DataMember]
        public int id_modela { get; set; }
        [DataMember]
        public int ura { get; set; }
        [DataMember]
        public int minuta { get; set; }
        [DataMember]
        public int dan { get; set; }
        [DataMember]
        public int mesec { get; set; }
        [DataMember]
        public int leto { get; set; }
        [DataMember]
        public string opis { get; set; }
        [DataMember]
        public string znamka { get; set; }
        [DataMember]
        public string model { get; set; }
        [DataMember]
        public string naziv { get; set; }
        [DataMember]
        public string naslov { get; set; }
        [DataMember]
        public string kraj { get; set; }
        [DataMember]
        public string ime { get; set; }
        [DataMember]
        public string priimek { get; set; }
    }

    [DataContract]
    public class Oseba
    {
        [DataMember]
        public int id_uporabnika { get; set; }
        [DataMember]
        public int sekundarni_id { get; set; }
        [DataMember]
        public int stanje { get; set; }
        [DataMember]
        public string ime { get; set; }
        [DataMember]
        public string priimek { get; set; }
        [DataMember]
        public int id_poslovalnice { get; set; }
    }

    [DataContract]
    public class Regist
    {
        [DataMember]
        public string ime { get; set; }
        [DataMember]
        public string priimek { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string telefon { get; set; }
        [DataMember]
        public string uporabnisko_ime { get; set; }
        [DataMember]
        public string geslo { get; set; }
    }
}
