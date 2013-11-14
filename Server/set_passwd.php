<?php
include('input.php');
include('check_input.php');

// name, passwd, newpasswd
$dat = "User/".$name.".dat";
@$array = file($dat);
if (count($array)<1){
echo "FEHLER\r\nFehlerhafte User-Datei!\r\n";
exit;
}
else
$array[0] = md5($newpasswd)+"\r\n";

$datei = fopen($dat, "w");
for ($i=0;$i<count($array);$i++){
fwrite($datei, $array[$i]);
}
fclose($datei);
echo "Neues Passwort gesetzt\r\n";
?>