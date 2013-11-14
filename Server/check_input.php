<?php
$dat = "User/".$name.".dat";
if (!file_exists($dat) ){
echo "FEHLER\r\nBenutzername existiert nicht!\r\n";
exit;
}

// prüfen ob Passwort richtig ist
@$m_array = file($dat);
if (count($m_array)<1){
echo "FEHLER\r\nBenutzerdatei ist fehlerhaft!\r\n";
exit;
}

if (!md5($passwd) == rtrim($m_array[0])){
echo "FEHLER\r\nPasswort ist falsch!\n";
exit;
}
?>