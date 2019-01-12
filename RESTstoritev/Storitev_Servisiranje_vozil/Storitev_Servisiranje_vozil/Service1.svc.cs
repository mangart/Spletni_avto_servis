using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Net;

namespace Storitev_Servisiranje_vozil
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        string constr = "Server=tcp:servisvozil.database.windows.net,1433;Initial Catalog=servisvozil;Persist Security Info=False;User ID= zangostic;Password= Racunalnik1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public string Blabla()
        {
            return "BlaBla";
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public int Registriraj(Regist oseba)
        {
            //string constr = "Server=tcp:servisvozil.database.windows.net,1433;Initial Catalog=servisvozil;Persist Security Info=False;User ID= zangostic;Password= Racunalnik1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            int id_uporabnika = -1;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    byte[] salt;
                    new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                    var pbkdf2 = new Rfc2898DeriveBytes(oseba.geslo, salt, 10000);
                    byte[] hash = pbkdf2.GetBytes(20);
                    byte[] hashBytes = new byte[36];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);
                    string savedPasswordHash = Convert.ToBase64String(hashBytes);
                    using (SqlCommand command = new SqlCommand("INSERT INTO Uporabnik VALUES(@uime,@geslo)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@uime", oseba.uporabnisko_ime));
                        command.Parameters.Add(new SqlParameter("@geslo", savedPasswordHash));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    if (er.Message.StartsWith("Violation of UNIQUE KEY constraint"))
                    {
                        conn.Close();
                        return 2;
                        // handle your violation
                    }
                    else
                    {
                        conn.Close();
                        return 1;
                    }
                }
                try
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Uporabnik WHERE uporabnisko_ime = @uime", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@uime", oseba.uporabnisko_ime));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id_uporabnika = (int)reader[0];
                                break;
                            }
                        }
                    }
                }
                catch (SqlException er)
                {
                    conn.Close();
                    return 4;
                }
                try
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO Stranka VALUES(@ime,@priimek,@email,@telefon,@idupo)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@ime", oseba.ime));
                        command.Parameters.Add(new SqlParameter("@priimek", oseba.priimek));
                        command.Parameters.Add(new SqlParameter("@email", oseba.email));
                        command.Parameters.Add(new SqlParameter("@telefon", oseba.telefon));
                        command.Parameters.Add(new SqlParameter("@idupo", id_uporabnika));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    conn.Close();
                    return 1;
                }
                conn.Close();
            }
            return 0;
        }

        private bool AuthenticateUser()
        {
            WebOperationContext ctx = WebOperationContext.Current;
            string authHeader = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization];
            if (authHeader == null)
                return false;

            string[] loginData = authHeader.Split(':');
            if (loginData.Length == 2 && Login(loginData[0], loginData[1]))
                return true;
            return false;
        }

        public bool Login(string upoime, string geslo)
        {
            int idupor = -1;
            int jevredu = 1;
            using (SqlConnection conn = new SqlConnection())
            {
                //string constr = "Server=tcp:servisvozil.database.windows.net,1433;Initial Catalog=servisvozil;Persist Security Info=False;User ID= zangostic;Password= Racunalnik1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    string savedPasswordHash = "";
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Uporabnik WHERE uporabnisko_ime = @uime", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@uime", upoime));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                idupor = (int)reader[0];
                                savedPasswordHash = (string)reader[2];
                            }
                        }
                    }
                    if (idupor == -1 && savedPasswordHash == "")
                    {
                        return false;
                    }
                    byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                    byte[] salt = new byte[16];
                    Array.Copy(hashBytes, 0, salt, 0, 16);
                    /* Compute the hash on the password the user entered */
                    var pbkdf2 = new Rfc2898DeriveBytes(geslo, salt, 10000);
                    byte[] hash = pbkdf2.GetBytes(20);
                    /* Compare the results */
                    for (int i = 0; i < 20; i++)
                    {
                        if (hashBytes[i + 16] != hash[i])
                        {
                            jevredu = 0;
                            break;
                        }
                    }
                    if (jevredu == 1)
                    {
                        return true;
                    }
                }
                catch (SqlException er)
                {
                    System.Diagnostics.Debug.WriteLine("Napaka je: " + er.Message);
                    return false;
                }

                conn.Close();
            }
            return false;
        }

        public Oseba get_oseba()
        {
            Oseba oseba = new Oseba();
            if (!AuthenticateUser())
            {
                Oseba bla = new Oseba();
                bla.ime = "Napačno uporabniško ime ali geslo";
                bla.priimek = "Napačno uporabniško ime ali geslo";
                return bla;
            }
            int idupor = -1;
            int sekundID = -1;
            string ime = "";
            string priimek = "";
            int poslovalnicaID = -1;
            WebOperationContext ctx = WebOperationContext.Current;
            string authHeader = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization];
            if (authHeader == null)
                return null;
            string[] loginData = authHeader.Split(':');
            string upoime = loginData[0];
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Uporabnik WHERE uporabnisko_ime = @uime", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@uime", upoime));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                idupor = (int)reader[0];
                            }
                        }
                    }
                    oseba.id_uporabnika = idupor;
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Stranka WHERE ID_uporabnika = @uid", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@uid", idupor));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                sekundID = (int)reader[0];
                                ime = (string)reader[1];
                                priimek = (string)reader[2];
                            }
                        }
                    }
                    if (sekundID > 0)
                    {
                        oseba.sekundarni_id = sekundID;
                        oseba.stanje = 1;
                        oseba.ime = ime;
                        oseba.priimek = priimek;
                        oseba.id_poslovalnice = -1;
                        return oseba;
                    }
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Zaposleni WHERE ID_uporabnika = @uid", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@uid", idupor));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                sekundID = (int)reader[0];
                                ime = (string)reader[1];
                                priimek = (string)reader[2];
                                poslovalnicaID = (int)reader[3];
                            }
                        }
                    }
                    if(sekundID > 0)
                    {
                        oseba.sekundarni_id = sekundID;
                        oseba.stanje = 2;
                        oseba.ime = ime;
                        oseba.priimek = priimek;
                        oseba.id_poslovalnice = poslovalnicaID;
                        return oseba;
                    }




                }
                catch (SqlException er)
                {
                    System.Diagnostics.Debug.WriteLine("Napaka je: " + er.Message);
                }

                conn.Close();
            }
            return null;
        }

        public List<Vozilo> get_znamke()
        {
            List<Vozilo> retVal = new List<Vozilo>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                conn.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Znamka", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            retVal.Add(new Vozilo
                            {
                                id_znamke = Convert.ToInt32(reader[0]),
                                znamka = reader.GetString(1),
                            });
                        }
                    }
                }
                conn.Close();
            }
            return retVal;
        }

        public List<Vozilo> get_modeli(string id)
        {
            List<Vozilo> retVal = new List<Vozilo>();
            int zid;
            try
            {
                zid = Convert.ToInt32(id);
            }
            catch(Exception e)
            {
                zid = 0;
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                conn.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Model WHERE ID_znamke = @idz", conn))
                {
                    command.Parameters.Add(new SqlParameter("@idz", zid));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            retVal.Add(new Vozilo
                            {
                                id_modela = Convert.ToInt32(reader[0]),
                                model = reader.GetString(1),
                                id_znamke = zid
                            });
                        }
                    }
                }
                conn.Close();
            }
            return retVal;
        }

        public List<Poslovalnica> get_poslovalnice()
        {
            List<Poslovalnica> retVal = new List<Poslovalnica>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT p.ID_poslovalnice, p.naziv, p.naslov, k.Naziv_kraja FROM Poslovalnica p, Kraj k WHERE p.ID_kraja = k.ID_kraja", conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                retVal.Add(new Poslovalnica
                                {
                                    id_poslovalnice = Convert.ToInt32(reader[0]),
                                    naziv = reader.GetString(1),
                                    naslov = reader.GetString(2),
                                    kraj = reader.GetString(3)
                                });
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }
            return retVal;
        }

        public List<Vozilo> get_vozila(string id)
        {
            List<Vozilo> retVal = new List<Vozilo>();
            int sid;
            try
            {
                sid = Convert.ToInt32(id);
            }
            catch(Exception e)
            {
                sid = 0;
            }
            if (!AuthenticateUser())
            {
                retVal.Add(new Vozilo
                {
                    model = "Napačno uporabniško ime ali geslo"
                });
                return retVal;
            }
            Oseba oseba = get_oseba();
            if (oseba.stanje != 1 || oseba.sekundarni_id != sid)
            {
                retVal.Add(new Vozilo
                {
                    model = "Podani id se ne ujema z uporabniškim imenom in geslom"
                });
                return retVal;
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT v.id_vozila, v.letnica, v.id_znamke, v.id_modela, z.naziv_znamke, m.naziv_modela FROM vozilo v, model m, znamka z WHERE v.id_znamke = z.id_znamke and v.id_modela = m.id_modela and v.id_stranke = @ids; ", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@ids", sid));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                retVal.Add(new Vozilo
                                {
                                    id_vozila = reader.GetInt32(0),
                                    letnica = reader.GetInt32(1),
                                    id_znamke = reader.GetInt32(2),
                                    id_modela = reader.GetInt32(3),
                                    znamka = reader.GetString(4),
                                    model = reader.GetString(5),
                                });
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }

            return retVal;
        }

        public List<Narocilo> get_neodobrena_narocila(string id)
        {
            List<Narocilo> retVal = new List<Narocilo>();
            int sid;
            try
            {
                sid = Convert.ToInt32(id);
            }
            catch (Exception e)
            {
                sid = 0;
            }
            if (!AuthenticateUser())
            {
                retVal.Add(new Narocilo
                {
                    model = "Napačno uporabniško ime ali geslo"
                });
                return retVal;
            }
            Oseba oseba = get_oseba();
            if (oseba.stanje != 1 || oseba.sekundarni_id != sid)
            {
                retVal.Add(new Narocilo
                {
                    model = "Podani id se ne ujema z uporabniškim imenom in geslom"
                });
                return retVal;
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT n.id_narocila, n.id_vozila, n.id_poslovalnice, n.id_znamke, n.id_modela, n.ura, n.minuta, n.dan, n.mesec, n.leto, n.opis, z.naziv_znamke, m.naziv_modela, p.naziv, p.naslov, k.naziv_kraja  FROM narocilo n, znamka z, model m, poslovalnica p, kraj k WHERE p.id_poslovalnice = n.id_poslovalnice and n.id_znamke = z.id_znamke and n.id_modela = m.id_modela and p.id_kraja = k.id_kraja and n.potrjen = 0 and n.id_stranke = @ids; ", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@ids", sid));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                retVal.Add(new Narocilo
                                {
                                    id_narocila = reader.GetInt32(0),
                                    id_vozila = reader.GetInt32(1),
                                    id_poslovalnice = reader.GetInt32(2),
                                    id_znamke = reader.GetInt32(3),
                                    id_modela = reader.GetInt32(4),
                                    ura = reader.GetInt32(5),
                                    minuta = reader.GetInt32(6),
                                    dan = reader.GetInt32(7),
                                    mesec = reader.GetInt32(8),
                                    leto = reader.GetInt32(9),
                                    opis = reader.GetString(10),
                                    znamka = reader.GetString(11),
                                    model = reader.GetString(12),
                                    naziv = reader.GetString(13),
                                    naslov = reader.GetString(14),
                                    kraj = reader.GetString(15)
                                });
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }

            return retVal;
        }

        public List<Narocilo> get_odobrena_narocila(string id)
        {
            List<Narocilo> retVal = new List<Narocilo>();
            int sid;
            try
            {
                sid = Convert.ToInt32(id);
            }
            catch (Exception e)
            {
                sid = 0;
            }
            if (!AuthenticateUser())
            {
                retVal.Add(new Narocilo
                {
                    model = "Napačno uporabniško ime ali geslo"
                });
                return retVal;
            }
            Oseba oseba = get_oseba();
            if (oseba.stanje != 1 || oseba.sekundarni_id != sid)
            {
                retVal.Add(new Narocilo
                {
                    model = "Podani id se ne ujema z uporabniškim imenom in geslom"
                });
                return retVal;
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT n.id_narocila, n.id_vozila, n.id_poslovalnice, n.id_znamke, n.id_modela, n.ura, n.minuta, n.dan, n.mesec, n.leto, n.opis, z.naziv_znamke, m.naziv_modela, p.naziv, p.naslov, k.naziv_kraja  FROM narocilo n, znamka z, model m, poslovalnica p, kraj k WHERE p.id_poslovalnice = n.id_poslovalnice and n.id_znamke = z.id_znamke and n.id_modela = m.id_modela and p.id_kraja = k.id_kraja and n.potrjen = 1 and n.id_stranke = @ids; ", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@ids", sid));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                retVal.Add(new Narocilo
                                {
                                    id_narocila = reader.GetInt32(0),
                                    id_vozila = reader.GetInt32(1),
                                    id_poslovalnice = reader.GetInt32(2),
                                    id_znamke = reader.GetInt32(3),
                                    id_modela = reader.GetInt32(4),
                                    ura = reader.GetInt32(5),
                                    minuta = reader.GetInt32(6),
                                    dan = reader.GetInt32(7),
                                    mesec = reader.GetInt32(8),
                                    leto = reader.GetInt32(9),
                                    opis = reader.GetString(10),
                                    znamka = reader.GetString(11),
                                    model = reader.GetString(12),
                                    naziv = reader.GetString(13),
                                    naslov = reader.GetString(14),
                                    kraj = reader.GetString(15)
                                });
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }

            return retVal;
        }

        public List<Narocilo> get_opravljena_narocila(string id)
        {
            List<Narocilo> retVal = new List<Narocilo>();
            int sid;
            try
            {
                sid = Convert.ToInt32(id);
            }
            catch (Exception e)
            {
                sid = 0;
            }
            if (!AuthenticateUser())
            {
                retVal.Add(new Narocilo
                {
                    model = "Napačno uporabniško ime ali geslo"
                });
                return retVal;
            }
            Oseba oseba = get_oseba();
            if (oseba.stanje != 1 || oseba.sekundarni_id != sid)
            {
                retVal.Add(new Narocilo
                {
                    model = "Podani id se ne ujema z uporabniškim imenom in geslom"
                });
                return retVal;
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT n.id_narocila, n.id_vozila, n.id_poslovalnice, n.id_znamke, n.id_modela, n.ura, n.minuta, n.dan, n.mesec, n.leto, n.opis, z.naziv_znamke, m.naziv_modela, p.naziv, p.naslov, k.naziv_kraja  FROM opravljena_narocila n, znamka z, model m, poslovalnica p, kraj k WHERE p.id_poslovalnice = n.id_poslovalnice and n.id_znamke = z.id_znamke and n.id_modela = m.id_modela and p.id_kraja = k.id_kraja and n.id_stranke = @ids; ", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@ids", sid));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                retVal.Add(new Narocilo
                                {
                                    id_narocila = reader.GetInt32(0),
                                    id_vozila = reader.GetInt32(1),
                                    id_poslovalnice = reader.GetInt32(2),
                                    id_znamke = reader.GetInt32(3),
                                    id_modela = reader.GetInt32(4),
                                    ura = reader.GetInt32(5),
                                    minuta = reader.GetInt32(6),
                                    dan = reader.GetInt32(7),
                                    mesec = reader.GetInt32(8),
                                    leto = reader.GetInt32(9),
                                    opis = reader.GetString(10),
                                    znamka = reader.GetString(11),
                                    model = reader.GetString(12),
                                    naziv = reader.GetString(13),
                                    naslov = reader.GetString(14),
                                    kraj = reader.GetString(15)
                                });
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }

            return retVal;
        }

        public List<Narocilo> get_neodobrena_narocila_poslovalnice(string idp)
        {
            int id_posl;
            List<Narocilo> retVal = new List<Narocilo>();
            try
            {
                id_posl = Convert.ToInt32(idp);
            }
            catch(Exception e)
            {
                id_posl = 0;
            }
            if (!AuthenticateUser())
            {
                retVal.Add(new Narocilo
                {
                    model = "Napačno uporabniško ime ali geslo"
                });
                return retVal;
            }
            Oseba oseba = get_oseba();
            if (oseba.stanje != 2 || oseba.id_poslovalnice != id_posl)
            {
                retVal.Add(new Narocilo
                {
                    model = "Podani id se ne ujema z uporabniškim imenom in geslom"
                });
                return retVal;
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT n.id_narocila, n.id_vozila, n.id_poslovalnice, n.id_znamke, n.id_modela, n.ura, n.minuta, n.dan, n.mesec, n.leto, n.opis, z.naziv_znamke, m.naziv_modela, p.naziv, p.naslov, k.naziv_kraja, s.ime, s.priimek  FROM narocilo n, znamka z, model m, poslovalnica p, kraj k, stranka s WHERE p.id_poslovalnice = n.id_poslovalnice and n.id_znamke = z.id_znamke and n.id_modela = m.id_modela and p.id_kraja = k.id_kraja and n.potrjen = 0 and n.id_stranke = s.id_stranke and n.id_poslovalnice = @idp; ", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idp", id_posl));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                retVal.Add(new Narocilo
                                {
                                    id_narocila = reader.GetInt32(0),
                                    id_vozila = reader.GetInt32(1),
                                    id_poslovalnice = reader.GetInt32(2),
                                    id_znamke = reader.GetInt32(3),
                                    id_modela = reader.GetInt32(4),
                                    ura = reader.GetInt32(5),
                                    minuta = reader.GetInt32(6),
                                    dan = reader.GetInt32(7),
                                    mesec = reader.GetInt32(8),
                                    leto = reader.GetInt32(9),
                                    opis = reader.GetString(10),
                                    znamka = reader.GetString(11),
                                    model = reader.GetString(12),
                                    naziv = reader.GetString(13),
                                    naslov = reader.GetString(14),
                                    kraj = reader.GetString(15),
                                    ime = reader.GetString(16),
                                    priimek = reader.GetString(17)
                                });
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }
            return retVal;
        }

        public List<Narocilo> get_odobrena_narocila_poslovalnice(string idp)
        {
            int id_posl;
            List<Narocilo> retVal = new List<Narocilo>();
            try
            {
                id_posl = Convert.ToInt32(idp);
            }
            catch (Exception e)
            {
                id_posl = 0;
            }
            if (!AuthenticateUser())
            {
                retVal.Add(new Narocilo
                {
                    model = "Napačno uporabniško ime ali geslo"
                });
                return retVal;
            }
            Oseba oseba = get_oseba();
            if (oseba.stanje != 2 || oseba.id_poslovalnice != id_posl)
            {
                retVal.Add(new Narocilo
                {
                    model = "Podani id se ne ujema z uporabniškim imenom in geslom"
                });
                return retVal;
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT n.id_narocila, n.id_vozila, n.id_poslovalnice, n.id_znamke, n.id_modela, n.ura, n.minuta, n.dan, n.mesec, n.leto, n.opis, z.naziv_znamke, m.naziv_modela, p.naziv, p.naslov, k.naziv_kraja, s.ime, s.priimek  FROM narocilo n, znamka z, model m, poslovalnica p, kraj k, stranka s WHERE p.id_poslovalnice = n.id_poslovalnice and n.id_znamke = z.id_znamke and n.id_modela = m.id_modela and p.id_kraja = k.id_kraja and n.potrjen = 1 and n.id_stranke = s.id_stranke and n.id_poslovalnice = @idp; ", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idp", id_posl));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                retVal.Add(new Narocilo
                                {
                                    id_narocila = reader.GetInt32(0),
                                    id_vozila = reader.GetInt32(1),
                                    id_poslovalnice = reader.GetInt32(2),
                                    id_znamke = reader.GetInt32(3),
                                    id_modela = reader.GetInt32(4),
                                    ura = reader.GetInt32(5),
                                    minuta = reader.GetInt32(6),
                                    dan = reader.GetInt32(7),
                                    mesec = reader.GetInt32(8),
                                    leto = reader.GetInt32(9),
                                    opis = reader.GetString(10),
                                    znamka = reader.GetString(11),
                                    model = reader.GetString(12),
                                    naziv = reader.GetString(13),
                                    naslov = reader.GetString(14),
                                    kraj = reader.GetString(15),
                                    ime = reader.GetString(16),
                                    priimek = reader.GetString(17)
                                });
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }
            return retVal;
        }

        public string Odobri_narocila(string idn)
        {
            int id_naroc;
            int id_posl = 0;
            int potrjen = 2;
            try
            {
                id_naroc = Convert.ToInt32(idn);
            }
            catch (Exception e)
            {
                id_naroc = 0;
            }
            if (!AuthenticateUser())
            {

                return "Napačno uporabniško ime ali geslo";
            }
            Oseba oseba = get_oseba();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT id_poslovalnice, potrjen FROM narocilo WHERE id_narocila = @idn; ", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idn", id_naroc));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id_posl = reader.GetInt32(0);
                                potrjen = reader.GetInt32(1);
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }
            if (oseba.stanje != 2 || oseba.id_poslovalnice != id_posl)
            {
                return "Nimate pravice posodabljati naročila iz drugih poslovalnic v katerih ne delate oziroma niste zaposleni v tej poslovalnici";
            }
            if(potrjen != 0)
            {
                return "Potrdite lahko samo še ne potrjena naročila!";
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("UPDATE Narocilo SET potrjen = 1 WHERE id_narocila = @idn", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idn", id_naroc));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "Vse je vredu!";
        }

        public string Opravi_narocila(string idn)
        {
            int id_naroc;
            int id_posl = 0;
            int potrjen = 2;
            try
            {
                id_naroc = Convert.ToInt32(idn);
            }
            catch (Exception e)
            {
                id_naroc = 0;
            }
            if (!AuthenticateUser())
            {

                return "Napačno uporabniško ime ali geslo";
            }
            Oseba oseba = get_oseba();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT id_poslovalnice, potrjen FROM narocilo WHERE id_narocila = @idn; ", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idn", id_naroc));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id_posl = reader.GetInt32(0);
                                potrjen = reader.GetInt32(1);
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }
            if (oseba.stanje != 2 || oseba.id_poslovalnice != id_posl)
            {
                return "Nimate pravice posodabljati naročila iz drugih poslovalnic v katerih ne delate oziroma niste zaposleni v tej poslovalnici";
            }
            if (potrjen != 1)
            {
                return "Opravite lahko samo že potrjena naročila!";
            }
            // Sedaj dodamo v tabelo opravljenih narocil in odstranimo iz tabele narocil
            int idNarocila = 0;
            int idPoslovalnice = 0;
            int idStranke = 0;
            int idVozila = 0;
            int idZnamke = 0;
            int idModela = 0;
            int ura = 0;
            int minuta = 0;
            int leto = 0;
            int dan = 0;
            int mesec = 0;
            potrjen = 0;
            string opis = "";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Narocilo where ID_narocila = @idn", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idn", idn));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                idNarocila = (int)reader[0];
                                idStranke = (int)reader[1];
                                idPoslovalnice = (int)reader[2];
                                idVozila = (int)reader[3];
                                idZnamke = (int)reader[4];
                                idModela = (int)reader[5];
                                ura = (int)reader[6];
                                minuta = (int)reader[7];
                                dan = (int)reader[8];
                                mesec = (int)reader[9];
                                leto = (int)reader[10];
                                potrjen = (int)reader[11];
                                opis = (string)reader[12];
                                break;
                            }
                        }
                    }
                    using (SqlCommand command = new SqlCommand("INSERT INTO Opravljena_narocila VALUES(@idn,@ids,@idp,@idv,@idz,@idm,@ura,@min,@dan,@mes,@let,@pot,@opi)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idn", idNarocila));
                        command.Parameters.Add(new SqlParameter("@ids", idStranke));
                        command.Parameters.Add(new SqlParameter("@idp", idPoslovalnice));
                        command.Parameters.Add(new SqlParameter("@idv", idVozila));
                        command.Parameters.Add(new SqlParameter("@idz", idZnamke));
                        command.Parameters.Add(new SqlParameter("@idm", idModela));
                        command.Parameters.Add(new SqlParameter("@ura", ura));
                        command.Parameters.Add(new SqlParameter("@min", minuta));
                        command.Parameters.Add(new SqlParameter("@dan", dan));
                        command.Parameters.Add(new SqlParameter("@mes", mesec));
                        command.Parameters.Add(new SqlParameter("@let", leto));
                        command.Parameters.Add(new SqlParameter("@pot", potrjen));
                        command.Parameters.Add(new SqlParameter("@opi", opis));
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand command = new SqlCommand("DELETE FROM Narocilo WHERE id_narocila = @idn", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idn", idn));
                        command.ExecuteNonQuery();
                    }


                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "Vse je vredu!";
        }

        public string Izbrisi_narocilo(string idn)
        {
            int id_naroc;
            int id_posl = 0;
            int id_stranke = 0;
            int potrjen = 2;
            try
            {
                id_naroc = Convert.ToInt32(idn);
            }
            catch (Exception e)
            {
                id_naroc = 0;
            }
            if (!AuthenticateUser())
            {

                return "Napačno uporabniško ime ali geslo";
            }
            Oseba oseba = get_oseba();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT id_poslovalnice, potrjen, id_stranke FROM narocilo WHERE id_narocila = @idn; ", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idn", id_naroc));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id_posl = reader.GetInt32(0);
                                potrjen = reader.GetInt32(1);
                                id_stranke = reader.GetInt32(2);
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }
            if (oseba.stanje == 2)
            {
                if (oseba.id_poslovalnice != id_posl)
                {
                    return "Nimate pravice brisati naročila iz drugih poslovalnic v katerih ne delate oziroma niste zaposleni v tej poslovalnici";
                }
            }
            if (oseba.stanje == 1)
            {
                if(oseba.sekundarni_id != id_stranke)
                {
                    return "Nimate pravice brisati narocil drugih strank!";
                }
                if (potrjen == 1)
                {
                    return "Zbrisete lahko samo se ne potrjne prijave na servis!";
                }
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Narocilo WHERE id_narocila = @idn", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idn", id_naroc));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "Vse je vredu!";
        }

        public string Izbrisi_vozilo(string idv)
        {
            int id_vozila;
            int id_stranke = 0;
            try
            {
                id_vozila = Convert.ToInt32(idv);
            }
            catch (Exception e)
            {
                id_vozila = 0;
            }
            if (!AuthenticateUser())
            {

                return "Napačno uporabniško ime ali geslo";
            }
            Oseba oseba = get_oseba();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT id_stranke FROM vozilo WHERE id_vozila = @idv; ", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idv", id_vozila));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id_stranke = reader.GetInt32(0);
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }
            if (oseba.stanje != 1)
            {
                return "Vozila lahko brišejo le stranke!";
            }
            if(oseba.sekundarni_id != id_stranke)
            {
                return "Id stranke (lastnika vozila) se ne ujema z vašim id-jem stranke!";
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Vozilo WHERE id_vozila = @idv", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idv", id_vozila));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "Vse je vredu!";
        }

        public string Dodaj_vozilo(Vozilo voz)
        {
            int pravaZnamka = 0;
            int praviModel = 0;
            int znamk = 0;
            if (!AuthenticateUser())
            {

                return "Napačno uporabniško ime ali geslo";
            }
            Oseba oseba = get_oseba();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM znamka WHERE id_znamke = @idz; ", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idz", voz.id_znamke));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                pravaZnamka = 1;
                            }
                        }
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            if(pravaZnamka == 0)
            {
                return "Znamka ne obstaja!";
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM model WHERE id_modela = @idm; ", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idm", voz.id_modela));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                praviModel= 1;
                            }
                        }
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            if(praviModel == 0)
            {
                return "Model ne obstaja!";
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT id_znamke FROM model WHERE id_modela = @idm; ", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idm", voz.id_modela));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                znamk = reader.GetInt32(0);
                            }
                        }
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            if(voz.id_znamke != znamk)
            {
                return "Model ne pripada podani znamki";
            }
            if (oseba.stanje != 1)
            {
                return "Vozila lahko dodajajo le stranke!";
            }
            if (voz.letnica < 1700 || voz.letnica > 3000)
            {
                return "Letnica ni veljavna!";
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO Vozilo VALUES(@let,@idz,@idm,@ids)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@let", voz.letnica));
                        command.Parameters.Add(new SqlParameter("@idz", voz.id_znamke));
                        command.Parameters.Add(new SqlParameter("@idm", voz.id_modela));
                        command.Parameters.Add(new SqlParameter("@ids", oseba.sekundarni_id));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "Vse je vredu!";
        }

        public string Naroci(Narocilo naroc)
        {
            int idStranke = 0;
            int idZnamke = 0;
            int idModela = 0;
            if (!AuthenticateUser())
            {

                return "Napačno uporabniško ime ali geslo";
            }
            Oseba oseba = get_oseba();
            if (naroc.ura < 0 || naroc.ura > 23)
            {
                return "Ura, ki ste jo podali je neveljavna!";
            }
            if (naroc.minuta < 0 || naroc.minuta > 59)
            {
                return "Minuta, ki ste jo podali je neveljavna!";
            }
            if (naroc.dan < 1 || naroc.dan > 31)
            {
                return "Dan, ki ste ga podali ni veljaven!";
            }
            if (naroc.mesec < 1 || naroc.mesec > 12)
            {
                return "Mesec, ki ste ga podali ni veljaven!";
            }
            if(naroc.leto < 2019)
            {
                return "Leto, ki ste ga podali ni veljavno!";
            }
            if((naroc.mesec == 4 || naroc.mesec == 6 || naroc.mesec == 9 || naroc.mesec == 11) && naroc.dan > 30)
            {
                return "Podani mesec nima več kot 30 dni!";
            }
            else if(naroc.mesec == 2 && naroc.dan > 28)
            {
                return "Podani mesec nima več kot 28 dni!";
            }
            if(oseba.stanje != 1)
            {
                return "Naročilo na servis lahko dodajo samo stranke!";
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT id_stranke FROM vozilo WHERE id_vozila = @idv; ", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idv", naroc.id_vozila));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                idStranke = reader.GetInt32(0);
                            }
                        }
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            if(oseba.sekundarni_id != idStranke)
            {
                return "Na servis lahko naročite le vaša vozila!";
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT id_znamke, id_modela FROM vozilo WHERE id_vozila = @idv; ", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idv", naroc.id_vozila));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                idZnamke = reader.GetInt32(0);
                                idModela = reader.GetInt32(1);
                            }
                        }
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            if(idZnamke == 0 || idModela == 0)
            {
                return "Id znamke ali modela ne obstaja!";
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO Narocilo VALUES(@ids,@idp,@idv,@idz,@idm,@ura,@min,@dan,@mes,@let,0,@opi)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@ids", oseba.sekundarni_id));
                        command.Parameters.Add(new SqlParameter("@idp", naroc.id_poslovalnice));
                        command.Parameters.Add(new SqlParameter("@idv", naroc.id_vozila));
                        command.Parameters.Add(new SqlParameter("@idz", idZnamke));
                        command.Parameters.Add(new SqlParameter("@idm", idModela));
                        command.Parameters.Add(new SqlParameter("@ura", naroc.ura));
                        command.Parameters.Add(new SqlParameter("@min", naroc.minuta));
                        command.Parameters.Add(new SqlParameter("@dan", naroc.dan));
                        command.Parameters.Add(new SqlParameter("@mes", naroc.mesec));
                        command.Parameters.Add(new SqlParameter("@let", naroc.leto));
                        command.Parameters.Add(new SqlParameter("@opi", naroc.opis));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "Vse je vredu!";
        }
    }
}
