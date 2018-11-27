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


    }
}