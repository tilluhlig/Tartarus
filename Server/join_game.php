<?php
include('input.php');
include('check_input.php');
include('check_gameid.php');
// name, passwd, gameid
$dat = "Games/".$gameid."/GameInfo.dat";
@$m_array = file($dat);

if (rtrim($m_array[2])!=""){
echo "FEHLER\r\nSpiel ist bereits voll!\r\n";
exit;
}

if (rtrim($m_array[1])==$name){
echo "FEHLER\r\nSpieler bereits vorhanden!\r\n";
exit;
}

$dat2 = "OffeneSpiele.txt";
if (!file_exists($dat2) ){
echo "FEHLER\r\nOffene Spiele existieren nicht!\r\n";
exit;
}

$dat3 = "Spiele.txt";
if (!file_exists($dat3) ){
echo "FEHLER\r\nSpiele existieren nicht!\r\n";
exit;
}

// beitreten
if (rtrim($m_array[0])=="")$m_array[0] = $name."\r\n";
$m_array[2] = $name."\r\n";
$datei = fopen($dat, "w");
for ($i=0;$i<count($m_array);$i++){
fwrite($datei, $m_array[$i]);
}
fclose($datei);

// Spiel eintragen
@$array = file($dat3);
$datei = fopen($dat3, "w");
for ($i=0;$i<count($array);$i=$i+4){
if (rtrim($array[$i])!=$gameid){
fwrite($datei,$array[$i]);
fwrite($datei,$array[$i+1]);
fwrite($datei,$array[$i+2]);
fwrite($datei,$array[$i+3]);
}
else{
fwrite($datei,$array[$i]);
fwrite($datei,$array[$i+1]);
fwrite($datei,$name."\r\n");
fwrite($datei,$array[$i+3]);
}
}
fclose($datei);


// offenes Spiel löschen
@$array = file($dat2);
$datei = fopen($dat2, "w");
for ($i=0;$i<count($array);$i=$i+3){
if (rtrim($array[$i])!=$gameid){
fwrite($datei,$array[$i]);
fwrite($datei,$array[$i+1]);
fwrite($datei,$array[$i+2]);
}
}
fclose($datei);

// in Spielerdatei eintragen
@$array = file("User/".$name.".dat");
$datei = fopen("User/".$name.".dat", "w");
for ($i=0;$i<count($array);$i++){
if (rtrim($array[$i])=="<games>"){
fwrite($datei,$array[$i]);
for ($b=$i+1;$b<count($array);$b++){
if (rtrim($array[$b])=="<endgames>"){
fwrite($datei,"$gameid\r\n");
fwrite($datei,"<endgames>\r\n");
for ($c=$b+2;$c<count($array);$c++){
fwrite($datei,$array[$c]);
}
break;
}
fwrite($datei,$array[$b]);
}
break;
}
fwrite($datei,$array[$i]);
}
fclose($datei);

echo "Spiel beigetreten\r\n";
?>