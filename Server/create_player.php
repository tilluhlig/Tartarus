<?php
include('input.php');

// name, passwd, email
$dat = "User/".$name.".dat";
if (file_exists($dat) ){
echo "FEHLER\r\nBenutzername existiert bereits!\r\n";
exit;
}

// Nutzer anlegen
$datei = fopen($dat, "w");
fwrite($datei, md5($passwd)."\r\n");
fwrite($datei, "$email\r\n");
fwrite($datei, "<games>\r\n");
fwrite($datei, "<endgames>\r\n");
fclose($datei);

chmod($dat, 0777);

echo "Spieler wurde erstellt\r\n";
?>