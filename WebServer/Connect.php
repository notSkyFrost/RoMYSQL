<?php
$Script = filter_input(INPUT_POST, 'Script');
$Host = "";
$UsuarioDB = "";
$ContraseñaDB = "";
$NombreDB = "";
$solve = str_replace("'", "\'", $Script);

$Conn = new mysqli($Host, $UsuarioDB, $ContraseñaDB, $NombreDB);
if (mysqli_connect_errno()) {
    printf("Conexión fallida: %s\n", mysqli_connect_error());
}
    else{
        $sql = "INSERT INTO Scripts (Script) VALUES ('$solve')";
        if ($Conn->query($sql) === TRUE) {
            header("Location: https://pruebaejecucionremota.000webhostapp.com/executed.html");
            die();
        } else {
            echo "Error: " . $sql . "<br>" . $Conn->error;
        }
        
    }

?>
