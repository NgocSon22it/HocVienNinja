using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UnityEngine;

public class CharacterDAO : MonoBehaviour 
{
    string ConnectionStr = "Server=(local);uid=sa;pwd=123456;Database=Ninja;Trusted_Connection=True;";

    public List<CharacterEntity> GetAllCharacter()
    {
        List<CharacterEntity> list = new List<CharacterEntity>();
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from [Character]";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    list.Add(new CharacterEntity
                    {
                        CharacterID = Convert.ToInt32(dr["Cha_ID"]),
                        CharacterName = dr["Name"].ToString(),
                        TotalHealthPoint = Convert.ToInt32(dr["Health"]),
                        TotalChakra = Convert.ToInt32(dr["Chakra"]),
                        CharacterDamage = Convert.ToInt32(dr["Damage"]),
                        CharacterSpeed = Convert.ToInt32(dr["Speed"]),
                        Description = dr["Description"].ToString(),
                        AbilitiesID = Convert.ToInt32(dr["Abi_ID"]),
                        CharacterImage = dr["Link"].ToString()
                    });
                }

            }
            finally
            {
                connection.Close();

            }
        }

        return list;
    }
    public CharacterEntity GetCharacterbyID(int id)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from [Character] where Cha_ID = " + id;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    CharacterEntity a = new CharacterEntity
                    {
                        CharacterID = Convert.ToInt32(dr["Cha_ID"]),
                        CharacterName = dr["Name"].ToString(),
                        TotalHealthPoint = Convert.ToInt32(dr["Health"]),
                        TotalChakra = Convert.ToInt32(dr["Chakra"]),
                        CharacterDamage = Convert.ToInt32(dr["Damage"]),
                        CharacterSpeed = Convert.ToInt32(dr["Speed"]),
                        Description = dr["Description"].ToString(),
                        AbilitiesID = Convert.ToInt32(dr["Abi_ID"]),
                        CharacterImage = dr["Link"].ToString()
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
