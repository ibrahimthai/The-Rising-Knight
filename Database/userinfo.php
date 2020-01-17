<?php

	$con = mysqli_connect('localhost', 'root', 'root', 'unityaccess');

	// Check if connection happened
	if (mysqli_connect_errno()) 
	{
		echo "1: Connection failed"; // Error code if connection fails
		exit();
	}

	$username = $_POST["username"];

	// Check if name exists
	$namecheckquery = "SELECT username FROM userinfo WHERE username='" . $username . "';";

	$namecheck = mysqli_query($con, $namecheckquery) or die("2: Name check query failed");

	if (mysqli_num_rows($namecheck) > 0) 
	{
		echo "3: Username already exists";
		exit();
	}

	// Add username to the database table
	$insertusername = "INSERT INTO userinfo (username) VALUES ('"  . $username . "');";
	mysqli_query($con, $insertusername) or die("4: Insert userinfo failed"); // Error code if insertion fails

	echo("0");


?>