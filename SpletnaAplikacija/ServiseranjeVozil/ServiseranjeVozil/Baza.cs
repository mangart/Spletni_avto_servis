using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Linq;
using System.Web;

namespace ServiseranjeVozil
{
    public class Baza
    {
        protected static string constr = "Server=tcp:servisvozil.database.windows.net,1433;Initial Catalog=servisvozil;Persist Security Info=False;User ID= zangostic;Password= Racunalnik1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public static string NekaFunkcija()
        {
            return "Neki!";
        }

        public static int registriraj(string upoime, string geslo, string ime, string priimek, string email, string telefon)
        {
            int id_uporabnika = -1;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    byte[] salt;
                    new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                    var pbkdf2 = new Rfc2898DeriveBytes(geslo, salt, 10000);
                    byte[] hash = pbkdf2.GetBytes(20);
                    byte[] hashBytes = new byte[36];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);
                    string savedPasswordHash = Convert.ToBase64String(hashBytes);
                    using (SqlCommand command = new SqlCommand("INSERT INTO Uporabnik VALUES(@uime,@geslo)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@uime", upoime));
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
                        command.Parameters.Add(new SqlParameter("@uime", upoime));
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
                catch(SqlException er)
                {
                    conn.Close();
                    return 4;
                }
                try
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO Stranka VALUES(@ime,@priimek,@email,@telefon,@idupo)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@ime", ime));
                        command.Parameters.Add(new SqlParameter("@priimek", priimek));
                        command.Parameters.Add(new SqlParameter("@email", email));
                        command.Parameters.Add(new SqlParameter("@telefon", telefon));
                        command.Parameters.Add(new SqlParameter("@idupo", id_uporabnika));
                        command.ExecuteNonQuery();
                    }
                }
                catch(SqlException er)
                {
                    conn.Close();
                    return 1;
                }
                conn.Close();
            }
            return 0;
        }


    public static int validiraj(string upoime, string geslo)
        {
            int idupor = -1;
            int jevredu = 1;
            using (SqlConnection conn = new SqlConnection())
            {
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
                        return -2;
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
                        return idupor;
                    }
                }
                catch (SqlException er)
                {
                    System.Diagnostics.Debug.WriteLine("Napaka je: " + er.Message);
                    return -1;
                }

                conn.Close();
            }
            return 0;
        }

        public static int dobiStanje(int idup)
        {
            int jeStranka = 0;
            int id = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Stranka where ID_uporabnika = @idu", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idu", idup));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id = (int)reader[0];
                            }
                        }
                    }
                }
                catch(SqlException er)
                {
                    return -1;
                }
                conn.Close();
            }
            if(id > 0)
            {
                jeStranka = 1;
            }
            return jeStranka;
        }

        public static Dictionary<string, int> DobiZnamke()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
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
                            dictionary.Add((string)reader[1], (int)reader[0]);
                        }
                    }
                }
                conn.Close();
            }
            return dictionary;
        }

        public static Dictionary<string, int> DobiModele(int idZnamka)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                conn.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Model where ID_znamke = @idz", conn))
                {
                    command.Parameters.Add(new SqlParameter("@idz", idZnamka));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dictionary.Add((string)reader[1], (int)reader[0]);
                        }
                    }
                }
                conn.Close();
            }
            return dictionary;
        }

        public static string VnesiVozilo(int idz, int idm, int letnica, int ids)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO Vozilo VALUES(@let,@idz,@idm,@ids)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@let", letnica));
                        command.Parameters.Add(new SqlParameter("@idz", idz));
                        command.Parameters.Add(new SqlParameter("@idm", idm));
                        command.Parameters.Add(new SqlParameter("@ids", ids));
                        command.ExecuteNonQuery();
                    }
                }
                catch(SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "Vse je vredu";
        }

        public static int DobiStranko(int idu)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Stranka where ID_uporabnika = @idu", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idu", idu));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id = (int)reader[0];
                            }
                        }
                    }
                }
                catch (SqlException er)
                {
                    return -1;
                }
                conn.Close();
            }
            return id;
        }

        public static Dictionary<string, int> DobiPoslovalnice()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Poslovalnica", conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dictionary.Add((string)reader[1], (int)reader[0]);
                            }
                        }
                    }
                }
                catch(SqlException er)
                {

                }
                conn.Close();
            }
            return dictionary;
        }

        public static Dictionary<string, int> DobiVozila(int ids)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT v.id_vozila, z.naziv_znamke, m.naziv_modela, v.letnica  FROM vozilo v, znamka z, model m WHERE v.id_stranke = @ids and v.id_znamke = z.id_znamke and m.id_modela = v.id_modela;", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@ids", ids));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string neki = "";
                                neki = neki + (string)reader[1] + ", " + (string)reader[2] + ", " + reader[3].ToString();
                                dictionary.Add(neki, (int)reader[0]);
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }
            return dictionary;
        }

        public static string VnesiNarocilo(int idv, int idp, int ura, int min, int dan, int mes, int leto, string opis)
        {
            int idznamke = 0;
            int idmodela = 0;
            int idstranke = 0;
            //  v bazo se zapiše id voila, id znamke, id modela,  id poslovalnice, id stranke, ura, minuta, leto, mesec dan, opis
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT *FROM vozilo WHERE id_vozila = @idv", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idv", idv));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                idznamke = (int)reader[2];
                                idmodela = (int)reader[3];
                                idstranke = (int)reader[4];
                                break;
                            }
                        }
                    }
                    using (SqlCommand command = new SqlCommand("INSERT INTO Narocilo(ID_stranke,ID_poslovalnice,ID_vozila,ID_znamke,ID_modela,ura,minuta,dan,mesec,leto,potrjen,opis) VALUES(@ids,@idp,@idv,@idz,@idm,@ura,@min,@dan,@mes,@let,0,@opi)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@ids", idstranke));
                        command.Parameters.Add(new SqlParameter("@idp", idp));
                        command.Parameters.Add(new SqlParameter("@idv", idv));
                        command.Parameters.Add(new SqlParameter("@idz", idznamke));
                        command.Parameters.Add(new SqlParameter("@idm", idmodela));
                        command.Parameters.Add(new SqlParameter("@ura", ura));
                        command.Parameters.Add(new SqlParameter("@min", min));
                        command.Parameters.Add(new SqlParameter("@dan", dan));
                        command.Parameters.Add(new SqlParameter("@mes", mes));
                        command.Parameters.Add(new SqlParameter("@let", leto));
                        command.Parameters.Add(new SqlParameter("@opi", opis));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "";
        }

        public static int PreveriServis(int idv)
        {
            int pot = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Narocilo where ID_vozila = @idv and potrjen = 1", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idv", idv));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return 0;
                            }
                            else
                            {
                                return 1;
                            }
                        }
                    }
                }
                catch (SqlException er)
                {
                    return -1;
                }
                conn.Close();
            }
            return 1;
        }

        public static string BrisiVozilo(int idv)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Vozilo WHERE id_vozila = @idv", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idv", idv));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "";
        }

        public static Dictionary<string, int> DobiNarocila(int ids)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("select n.id_narocila, p.naziv, z.naziv_znamke, m.naziv_modela, n.dan, n.mesec,n.leto,n.ura,n.minuta, n.opis from narocilo n, poslovalnica p, znamka z, model m, vozilo v where z.id_znamke = v.id_znamke and m.id_modela = v.id_modela and p.id_poslovalnice = n.id_poslovalnice and n.id_vozila = v.id_vozila and n.id_stranke = @ids and n.potrjen != 1", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@ids", ids));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string neki = "";
                                neki = neki + (string)reader[2] + ", " + reader[3].ToString() + " pri " + reader[1].ToString() + " na datum: " + reader[4].ToString() + ". " + reader[5].ToString() + ". " + reader[6].ToString() + " ob " + reader[7].ToString() + ":" + reader[8].ToString() + " Opis: " + reader[9].ToString();
                                dictionary.Add(neki, (int)reader[0]);
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }
            return dictionary;
        }

        public static string BrisiNarocilo(int idn)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
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
            return "";
        }

        public static Dictionary<string, int> DobiOdobrenaNarocila(int ids)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("select n.id_narocila, p.naziv, z.naziv_znamke, m.naziv_modela, n.dan, n.mesec,n.leto,n.ura,n.minuta, n.opis from narocilo n, poslovalnica p, znamka z, model m, vozilo v where z.id_znamke = v.id_znamke and m.id_modela = v.id_modela and p.id_poslovalnice = n.id_poslovalnice and n.id_vozila = v.id_vozila and n.id_stranke = @ids and n.potrjen = 1", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@ids", ids));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string neki = "";
                                neki = neki + (string)reader[2] + ", " + reader[3].ToString() + " pri " + reader[1].ToString() + " na datum: " + reader[4].ToString() + ". " + reader[5].ToString() + ". " + reader[6].ToString() + " ob " + reader[7].ToString() + ":" + reader[8].ToString() + " Opis: " + reader[9].ToString();
                                dictionary.Add(neki, (int)reader[0]);
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }
            return dictionary;
        }

        public static Dictionary<string, int> DobiNarocilaZaOdobritev(int idp)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("select n.id_narocila, p.naziv, z.naziv_znamke, m.naziv_modela, n.dan, n.mesec,n.leto,n.ura,n.minuta, n.opis from narocilo n, poslovalnica p, znamka z, model m, vozilo v where z.id_znamke = v.id_znamke and m.id_modela = v.id_modela and p.id_poslovalnice = n.id_poslovalnice and n.id_vozila = v.id_vozila and p.id_poslovalnice = @idp and n.potrjen != 1", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idp", idp));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string neki = "";
                                neki = neki + (string)reader[2] + ", " + reader[3].ToString() + " pri " + reader[1].ToString() + " na datum: " + reader[4].ToString() + ". " + reader[5].ToString() + ". " + reader[6].ToString() + " ob " + reader[7].ToString() + ":" + reader[8].ToString() + " Opis: " + reader[9].ToString();
                                dictionary.Add(neki, (int)reader[0]);
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }
            return dictionary;
        }

        public static int DobiZaposlenega(int idu)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Zaposleni where ID_uporabnika = @idu", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idu", idu));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id = (int)reader[0];
                            }
                        }
                    }
                }
                catch (SqlException er)
                {
                    return -1;
                }
                conn.Close();
            }
            return id;
        }

        public static int DobiPoslovalnico(int idu)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Zaposleni where ID_uporabnika = @idu", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idu", idu));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                id = (int)reader[3];
                            }
                        }
                    }
                }
                catch (SqlException er)
                {
                    return -1;
                }
                conn.Close();
            }
            return id;
        }

        public static string OdobriNarocilo(int idn)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("UPDATE Narocilo SET potrjen = 1 WHERE id_narocila = @idn", conn))
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
            return "";
        }

        public static Dictionary<string,int> DobiNarocilaOdobrena(int idp)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("select n.id_narocila, p.naziv, z.naziv_znamke, m.naziv_modela, n.dan, n.mesec,n.leto,n.ura,n.minuta, n.opis from narocilo n, poslovalnica p, znamka z, model m, vozilo v where z.id_znamke = v.id_znamke and m.id_modela = v.id_modela and p.id_poslovalnice = n.id_poslovalnice and n.id_vozila = v.id_vozila and p.id_poslovalnice = @idp and n.potrjen = 1", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idp", idp));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string neki = "";
                                neki = neki + (string)reader[2] + ", " + reader[3].ToString() + " pri " + reader[1].ToString() + " na datum: " + reader[4].ToString() + ". " + reader[5].ToString() + ". " + reader[6].ToString() + " ob " + reader[7].ToString() + ":" + reader[8].ToString() + " Opis: " + reader[9].ToString();
                                dictionary.Add(neki, (int)reader[0]);
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }
            return dictionary;
        }

        public static string OpraviNarocilo(int idn)
        {
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
            int potrjen = 0;
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
            return "";
        }

        public static string VnesiZnamko(string znamka)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO Znamka VALUES(@znamka)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@znamka", znamka));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "";
        }

        public static string BrisiZnamko(int idz)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Znamka WHERE id_znamke = @idz", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idz", idz));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "";
        }

        public static string VnesiKraj(string kraj)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO Kraj VALUES(@kraj)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@kraj", kraj));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "";
        }

        public static string BrisiKraj(int idk)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Kraj WHERE id_kraja = @idk", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idk", idk));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return  "";
        }

        public static Dictionary<string, int> DobiKraje()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                conn.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Kraj", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dictionary.Add((string)reader[1], (int)reader[0]);
                        }
                    }
                }
                conn.Close();
            }
            return dictionary;
        }

        public static string VnesiModel(int idz, string model)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO Model VALUES(@model, @idz)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@model", model));
                        command.Parameters.Add(new SqlParameter("@idz", idz));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "";
        }

        public static Dictionary<int, string> DobiModeleBrezZnamke()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                conn.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Model", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dictionary.Add((int)reader[0], (string)reader[1]);
                        }
                    }
                }
                conn.Close();
            }
            return dictionary;
        }

        public static string BrisiModel(int idm)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM model WHERE id_modela = @idm", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idm", idm));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "";
        }

        public static string DodajPoslovalnico(int idk,string naziv, string naslov)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("INSERT INTO Poslovalnica VALUES(@naziv, @naslov,@idk)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@naziv", naziv));
                        command.Parameters.Add(new SqlParameter("@naslov", naslov));
                        command.Parameters.Add(new SqlParameter("@idk", idk));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "";
        }

        public static string BrisiPoslovalnico(int idp)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Poslovalnica WHERE id_poslovalnice = @idp", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idp", idp));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "";
        }

        public static int registrirajZaposlenega(string upoime, string geslo, string ime, string priimek, int idp)
        {
            int id_uporabnika = -1;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    byte[] salt;
                    new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                    var pbkdf2 = new Rfc2898DeriveBytes(geslo, salt, 10000);
                    byte[] hash = pbkdf2.GetBytes(20);
                    byte[] hashBytes = new byte[36];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);
                    string savedPasswordHash = Convert.ToBase64String(hashBytes);
                    using (SqlCommand command = new SqlCommand("INSERT INTO Uporabnik VALUES(@uime,@geslo)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@uime", upoime));
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
                        command.Parameters.Add(new SqlParameter("@uime", upoime));
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
                    using (SqlCommand command = new SqlCommand("INSERT INTO Zaposleni VALUES(@ime,@priimek,@idp,@idupo)", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@ime", ime));
                        command.Parameters.Add(new SqlParameter("@priimek", priimek));
                        command.Parameters.Add(new SqlParameter("@idp", idp));
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

        public static Dictionary<string, int> DobiUporabnike()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                conn.Open();
                using (SqlCommand command = new SqlCommand("SELECT u.id_uporabnika, u.uporabnisko_ime, s.email FROM uporabnik u LEFT JOIN stranka s ON u.id_uporabnika = s.id_uporabnika WHERE u.id_uporabnika > 1", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string neki = "";
                            if(reader[2].ToString() == "")
                            {
                                neki = neki + (string)reader[1] + ", " + "Zaposleni";
                            }
                            else
                            {
                                neki = neki + (string)reader[1] + ", " + "Stranka";
                            }
                            dictionary.Add(neki, (int)reader[0]);
                        }
                    }
                }
                conn.Close();
            }
            return dictionary;
        }

        public static string BrisiUporabnika(int idu)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("DELETE FROM Uporabnik WHERE id_uporabnika = @idu", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@idu", idu));
                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException er)
                {
                    return er.Message;
                }
                conn.Close();
            }
            return "";
        }

        public static Dictionary<string, int> DobiOpravljenaNarocila(int ids)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = constr;
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("select n.id_narocila, p.naziv, z.naziv_znamke, m.naziv_modela, n.dan, n.mesec,n.leto,n.ura,n.minuta, n.opis from Opravljena_narocila n, poslovalnica p, znamka z, model m, vozilo v where z.id_znamke = v.id_znamke and m.id_modela = v.id_modela and p.id_poslovalnice = n.id_poslovalnice and n.id_vozila = v.id_vozila and n.id_stranke = @ids and n.potrjen = 1", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@ids", ids));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string neki = "";
                                neki = neki + (string)reader[2] + ", " + reader[3].ToString() + " pri " + reader[1].ToString() + " na datum: " + reader[4].ToString() + ". " + reader[5].ToString() + ". " + reader[6].ToString() + " ob " + reader[7].ToString() + ":" + reader[8].ToString() + " Opis: " + reader[9].ToString();
                                dictionary.Add(neki, (int)reader[0]);
                            }
                        }
                    }
                }
                catch (SqlException er)
                {

                }
                conn.Close();
            }
            return dictionary;
        }



    }
}