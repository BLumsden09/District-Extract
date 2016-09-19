using System;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;



namespace district_extract
{
    class Program
    {
        static void Main(string[] args)
        {
            using (
                var conn = new SqlConnection("Server=0;Database=0;User ID=0;Password= 0;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;")
                )
            {
                conn.Open();

                bool quit = false;
                string choice;
                SqlCommand cmd = new SqlCommand();
                Console.WriteLine("<---------------------");
                Console.WriteLine("       District");
                Console.WriteLine("--------------------->");

                while (!quit)
                {
                    Console.WriteLine("Select by public, private, or none?");
                    string category = Console.ReadLine();
                    string date;
                    string code;
                    string codeCol;
                    if (category == "public")
                    {
                        Console.WriteLine("Select by code, date, both, or none?");
                        choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "code":
                                Console.WriteLine("Select by code1 or code2?");
                                codeCol = Console.ReadLine();
                                Console.WriteLine("Enter desired code");
                                code = Console.ReadLine();
                                cmd = new SqlCommand("SELECT * FROM District WHERE (District." + @codeCol + "='" + @code + "') AND category ='" + @category + "' FOR XML PATH('district'), ROOT('leaCEDS')", conn);
                                quit = true;
                                break;

                            case "date":
                                Console.WriteLine("Enter desired date using the format MM/DD/YYYY.");
                                date = Console.ReadLine();
                                cmd = new SqlCommand("SELECT * FROM District WHERE (District.date_created >= '" + @date + "' OR District.date_updated >= '" + @date + "') AND category ='" + @category + "' FOR XML PATH('district'), ROOT('leaCEDS')", conn);
                                quit = true;
                                break;

                            case "both":
                                Console.WriteLine("Selet by code1 or code2?");
                                codeCol = Console.ReadLine();
                                Console.WriteLine("Enter desired code");
                                code = Console.ReadLine();
                                Console.WriteLine("Enter desired date using the format MM/DD/YYYY.");
                                date = Console.ReadLine();
                                cmd = new SqlCommand("SELECT * FROM District WHERE (District." + @codeCol + "='" + @code + "') AND (District.date_created >= '" + @date + "' OR District.date_updated >= '" + @date + "') AND category ='" + @category + "'FOR XML PATH('district'), ROOT('leaCEDS')", conn);
                                quit = true;

                                break;

                            case "none":
                                cmd = new SqlCommand("SELECT * FROM District WHERE category ='" + @category + "' FOR XML PATH('district'), ROOT('leaCEDS')", conn);
                                quit = true;
                                break;

                            default:
                                Console.WriteLine("Unknown Command " + choice);
                                continue;
                        }
                    }
                    else if(category == "private")
                    {
                        Console.WriteLine("Select by code, date, both, or none?");
                        choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "code":
                                Console.WriteLine("Select by code1 or code2?");
                                codeCol = Console.ReadLine();
                                Console.WriteLine("Enter desired code");
                                code = Console.ReadLine();
                                cmd = new SqlCommand("SELECT * FROM District WHERE (District." + @codeCol + "='" + @code + "') AND category ='" + @category + "' FOR XML PATH('district'), ROOT('leaCEDS')", conn);
                                quit = true;
                                break;

                            case "date":
                                Console.WriteLine("Enter desired date using the format MM/DD/YYYY.");
                                date = Console.ReadLine();
                                cmd = new SqlCommand("SELECT * FROM District WHERE (District.date_created >= '" + @date + "' OR District.date_updated >= '" + @date + "') AND category ='" + @category + "' FOR XML PATH('district'), ROOT('leaCEDS')", conn);
                                quit = true;
                                break;

                            case "both":
                                Console.WriteLine("Selet by code1 or code2?");
                                codeCol = Console.ReadLine();
                                Console.WriteLine("Enter desired code");
                                code = Console.ReadLine();
                                Console.WriteLine("Enter desired date using the format MM/DD/YYYY.");
                                date = Console.ReadLine();
                                cmd = new SqlCommand("SELECT * FROM District WHERE (District." + @codeCol + "='" + @code + "') AND (District.date_created >= '" + @date + "' OR District.date_updated >= '" + @date + "') AND category ='" + @category + "'FOR XML PATH('district'), ROOT('leaCEDS')", conn);
                                quit = true;

                                break;

                            case "none":
                                cmd = new SqlCommand("SELECT * FROM District WHERE category ='" + @category + "' FOR XML PATH('district'), ROOT('leaCEDS')", conn);
                                quit = true;
                                break;

                            default:
                                Console.WriteLine("Unknown Command " + choice);
                                continue;
                        }
                    }
                    else if(category == "none")
                    {
                        Console.WriteLine("Select by code, date, both, or none?");
                        choice = Console.ReadLine();
                        switch (choice)
                        {
                            case "code":
                                Console.WriteLine("Select by code1 or code2?");
                                codeCol = Console.ReadLine();
                                Console.WriteLine("Enter desired code");
                                code = Console.ReadLine();
                                cmd = new SqlCommand("SELECT * FROM District WHERE (District." + @codeCol + "='" + @code + "') FOR XML PATH('district'), ROOT('leaCEDS')", conn);
                                quit = true;
                                break;

                            case "date":
                                Console.WriteLine("Enter desired date using the format MM/DD/YYYY.");
                                date = Console.ReadLine();
                                cmd = new SqlCommand("SELECT * FROM District WHERE (District.date_created >= '" + @date + "' OR District.date_updated >= '" + @date + "') FOR XML PATH('district'), ROOT('leaCEDS')", conn);
                                quit = true;
                                break;

                            case "both":
                                Console.WriteLine("Selet by code1 or code2?");
                                codeCol = Console.ReadLine();
                                Console.WriteLine("Enter desired code");
                                code = Console.ReadLine();
                                Console.WriteLine("Enter desired date using the format MM/DD/YYYY.");
                                date = Console.ReadLine();
                                cmd = new SqlCommand("SELECT * FROM District WHERE (District." + @codeCol + "='" + @code + "') AND (District.date_created >= '" + @date + "' OR District.date_updated >= '" + @date + "') FOR XML PATH('district'), ROOT('leaCEDS')", conn);
                                quit = true;
                                break;

                            case "none":
                                cmd = new SqlCommand("SELECT * FROM District FOR XML PATH('district'), ROOT('leaCEDS')", conn);
                                quit = true;
                                break;

                            default:
                                Console.WriteLine("Unknown Command " +choice);
                                continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unknown Command " + category);
                        continue;
                    }
                }
                using (cmd)
                {
                    using (var reader = cmd.ExecuteXmlReader())
                    {
                        var doc = new XDocument();
                        try
                        {
                            doc = XDocument.Load(reader);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("There are no entries that match the given parameters.");
                        }
                        string path = @"District." + DateTime.Now.ToString("yyyyMMdd") + ".xml";
                        using (var writer = new StreamWriter(path))
                        {
                            XNamespace ns = "http://specification.sifassociation.org/Implementation/na/3.2/html/CEDS/K12/K12_leaCEDS.html#LinkD";
                            var root = new XElement(ns + "leaCEDS");
                            int count = 0;

                            foreach (var d in doc.Descendants("district"))
                            {
                                string delete;
                                string street;
                                string street2;

                                /*Delete flag modification*/
                                if ((string)d.Element("delete_flag") == "Y")
                                {
                                    delete = "1";
                                }
                                else
                                {
                                    delete = "0";
                                }

                                /*street check for null*/
                                if ((string)d.Element("streetLine2") == null)
                                {
                                    street2 = string.Empty;
                                    street = ((string)d.Element("streetLine1") + street2);
                                }
                                else
                                {
                                    street2 = (string)d.Element("streetLine2");
                                    street = ((string)d.Element("streetLine1") + "," + street2);
                                }
                                count++;
                                root.Add(new XElement(ns + "district",
                                            new XElement(ns + "identification",
                                                new XElement(ns + "leaID", (string)d.Element("leaID")),
                                                new XElement(ns + "name", (string)d.Element("name")),
                                                new XElement(ns + "organizationType", (string)d.Element("organizationType"))
                                                ),
                                            new XElement(ns + "addressList",
                                                new XElement(ns + "address",
                                                    new XElement(ns + "street",
                                                        new XElement(ns + "line1", street)
                                                        ),
                                                    new XElement(ns + "city", (string)d.Element("city")),
                                                    new XElement(ns + "stateProvince", (string)d.Element("stateProvince")),
                                                    new XElement(ns + "postalCode", (string)d.Element("postalCode")),
                                                    new XElement(ns + "county", (string)d.Element("county"))
                                                    )
                                                ),
                                            new XElement(ns + "reference",
                                                    new XElement(ns + "NCESID", (string)d.Element("NCESID_district"))
                                                    ),
                                            new XElement(ns + "delete", delete)
                                                )
                                            );
                            }
                            root.Save(writer);
                            Console.WriteLine("" + count + " district records written");
                            Console.ReadLine();

                        }


                    }
                }
            }

        }
    }
}



