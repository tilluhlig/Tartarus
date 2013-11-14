<?php
include('input.php');
include('check_input.php');
include('check_gameid.php');
// name, passwd, gameid, map, data

$dat = "Games/".$gameid."/GameInfo.dat";
@$m_array = file($dat);
if (count($m_array)<1){
echo "FEHLER\r\nKein nächster Spieler vorhanden\r\n";
exit;
}

if (count($m_array)<3){
echo "FEHLER\r\nSpiel läuft noch nicht!\r\n";
exit;
}

if (rtrim($m_array[0]) != $name){
echo "FEHLER\r\nSpieler ist nicht am Zug\r\n";
exit;
}

// alles korrekt
$dat = "Games/".$gameid."/Map.dat";
$datei = fopen($dat, "w");
fwrite($datei, $map);
fclose($datei);
chmod($dat, 0777);

$dat = "Games/".$gameid."/Data.dat";
$datei = fopen($dat, "w");
fwrite($datei, $data);
fclose($datei);
chmod($dat, 0777);

// nächsten Spieler setzen
$dat = "Games/".$gameid."/GameInfo.dat";
if ($next=="1"){
@$m_array = file($dat);
if (rtrim($m_array[1]) == $name){
$m_array[0] = $m_array[2];
}
else{
$m_array[0] = $m_array[1];
}

$m_array[4] = "0\r\n";
$m_array[5] = "0\r\n";

$datei = fopen($dat, "w");
for ($i=0;$i<count($m_array);$i++){
fwrite($datei, $m_array[$i]);
}
fclose($datei);
}

echo "Upload erfolgreich\r\n";

?>