
using System;
using System.Data;
using System.Data.SqlClient;

public class PlayerDAO
{
    string con = "Server=(local);uid=sa;pwd=123456;Database=Ninja;Trusted_Connection=True;";

    public Player GetPlayerbyID(string Id)
    {
        using (SqlConnection connection = new SqlConnection(con))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from ThongTinTaiKhoan where ID_TaiKhoan = " + Id;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    Player a = new Player
                    {
                        CharacterID = dr["ID_TaiKhoan"].ToString(),
                        CharacterName = dr["MatKhau"].ToString(),
                        TotalHealthPoint = Convert.ToInt32(dr["NamSinh"]),
                        TotalChakra = Convert.ToInt32(dr["NamSinh"]),
                        CharacterDamage = Convert.ToInt32(dr["NamSinh"]),
                        CharacterImage = dr["SDT"].ToString(),
                        Description = dr["DiaChi"].ToString(),
                        CharacterSpeed = Convert.ToInt32(dr["Tien"])

                    };
                    connection.Close();

                    return a;
                }

            }
            finally
            {
                connection.Close();

            }
        }

        return null;
    }



}
