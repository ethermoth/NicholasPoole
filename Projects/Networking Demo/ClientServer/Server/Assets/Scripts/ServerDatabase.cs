using System;
using UnityEngine;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using LitJson;

using MySql.Data;
using MySql.Data.MySqlClient;

public class ServerDatabase : MonoBehaviour {

    public static ExampleServer examServer;

    public static ServerNetwork serverNet;

    public long activePlayer;
    public string playerName;
    public int playerScore;

    public string host, database, user, password;
    public bool pooling = true;

    private string connectionString;

    private MySqlConnection con = null;
    public bool conCheck = false;//checks for an existing connection.
    
    private MySqlCommand cmd = null;
   
    private MySqlDataReader rdr = null;
    public bool rdrCheck = false;//checks if the reader is still running.

    private MD5 _md5hash;


    public List<int> everyPlayerID;//every element is a unique player's netID

    public List<int> everyPlayerStats;//every element ePIDC[x] corresponds to ePID[x].
    //Stores the player's state in the form of an integer.
    //integer is 10(team) + item, ex. "11" refers to a member of red team carrying a rocket.
    //team referred to as "epstats / 10"
    //item referred to by "epstats % 10"

    
	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this.gameObject);

        connectionString = "Server=" + host + ";Database=" + database + ";User=" + user + ";Password=" + password + ";Pooling=";
        if (pooling)
        {
            connectionString += "true";
        }
        else
        {
            connectionString += "false";
        }

        try
        {
            con = new MySqlConnection(connectionString);
           
            con.Open();
            
            Debug.Log("Client list state: " + con.State);
            
            string sql = "SELECT * FROM 'allclients'";
            
            cmd = new MySqlCommand(sql, con);
            
            
        }
        catch (Exception e)
        {
            Debug.Log("Mysql state: " + con.State);
            Debug.Log(e);
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (con != null)
        {
            conCheck = true;
        }
        else conCheck = false;

       

        if (rdr != null)
        {
            rdrCheck = true;
        }
        else rdrCheck = false;
    }

    public void AddNewPlayer(long newID, string name)
    {
        try
        {
            string cmdStr = "INSERT INTO `350_server`.`allclients` (`id`, `clientlist`, `score`, `playername`) VALUES ('0', '" + newID + "', '0', '" + name + "')";
            cmd = new MySqlCommand(cmdStr, con);
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    Debug.Log("Welcome!");
                }

            }
            rdr.Close();
            GetPlayer(newID, name);
        }//Create new entry in the 'players' table
        catch(Exception e)
        {
            Debug.Log("Failed to add new player: " + e);
        }
    }

    public void GetPlayer(long newID, string name)
    {
        try
        {
            //Checks for the existence of a newID within the table
            string cmdStr = "SELECT `id` FROM `allclients` WHERE `playername`='" + name + "'";
            cmd = new MySqlCommand(cmdStr, con);
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    Debug.Log("Welcome Back!");
                    if (rdr.GetInt32(0) != 0)
                    {
                        activePlayer = rdr.GetInt32(0);
                        //playerScore = rdr.GetInt32(rdr.GetOrdinal("score"));
                        // playerName = rdr.GetString(0);
                    }
                }
            }
           
            else
            {
                rdr.Close();
                AddNewPlayer(newID, name);
            }
            rdr.Close();
        }
        catch (MySqlException e)
        {
            Debug.Log("New Participant? " + e);
        }
    }

    public void AddScore(string name)
    {
        try
        {
            //Add 1 to the score value of a column
            string modTest = "UPDATE `allclients` SET `score` = `score`+'1' WHERE `playername` = '" + name + "'";
            cmd = new MySqlCommand(modTest, con);
            Debug.Log("Score Added to user " + name);
            rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read()) {}
            }
            rdr.Close();
        }
        catch (MySqlException e)
        {
            Debug.Log("No score for you! " + e);
        }

    }

    public void ResetScore(string name)
    {//this function, when called by a server admin, can reset the score of a player to 0.
        //This can't be called by the client under normal means
        try
        {
            //Add 1 to the score value of a column
            string resetStr = "UPDATE `allclients` SET `score` = '0' WHERE `playername` = '" + name + "'";
            cmd = new MySqlCommand(resetStr, con);
            rdr = cmd.ExecuteReader();
            Debug.Log("Score reset for user " + name);
            if (rdr.HasRows)
            {
                while (rdr.Read()) {}
            }
            rdr.Close();
        }
        catch (MySqlException e)
        {
            Debug.Log("Reset failed! " + e);
        }

    }

    public void OnQuit()
    {

    }

    void ClearTeam(int target)
    {
        target %= 10;
    }

    void ClearItem(int target)
    {
        target /= 10;
        target *= 10;
    }

    //			string sql = "SELECT * FROM players";
    //			cmd = new MySqlCommand(sql, con);
    //			rdr = cmd.ExecuteReader();
    //
    //          select, insert(if new), update(if not new)

    //			while (rdr.Read())
    //			{
    //				Debug.Log("???");
    //				Debug.Log(rdr[0]+" -- "+rdr[1]);
    //			}
    //			rdr.Close();

    /*
     void onApplicationQuit(){
		if (con != null) {
			if (con.State.ToString () != "Closed") {
				con.Close ();
				Debug.Log ("Mysql connection closed");
			}
			con.Dispose ();
		}
	}

	public string getFirstShops(){
		using (rdr = cmd.ExecuteReader ()) {
			while (rdr.Read ()) {
				return rdr [0] + " -- " + rdr [1];
			}
		}
		return "empty";
	}
	public string GetConnectionState(){
		return con.State.ToString ();
	}
}
     */
}
