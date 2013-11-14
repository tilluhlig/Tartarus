<?php

include('function.php');
include('input.php');
include('check_input.php');
include('check_gameid.php');
// name, passwd, gameid

// nimmt Spieler an dem Spiel teil?

$dat = "Games/".$gameid."/GameInfo.dat";
@$array = file($dat);
if (!rtrim($array[1])==$name && !rtrim($array[2])==$name){
echo "FEHLER\r\nSpieler nimmt nicht am Spiel teil!\n";
exit;
}


// offenes Spiel löschen
$dat2 = "OffeneSpiele.txt";
if (!file_exists($dat2) ){
echo "FEHLER\r\nOffene Spiele existieren nicht!\r\n";
exit;
}

if (!file_exists("Spiele.txt") ){
echo "FEHLER\r\nSpiele existieren nicht!\r\n";
exit;
}

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

$spielerA = $name;
// Spiel löschen
$dat2 = "Spiele.txt";

@$array = file($dat2);
$datei = fopen($dat2, "w");
for ($i=0;$i<count($array);$i=$i+4){
if (rtrim($array[$i])!=$gameid){
fwrite($datei,$array[$i]);
fwrite($datei,$array[$i+1]);
fwrite($datei,$array[$i+2]);
fwrite($datei,$array[$i+3]);
}
else{
$spielerA = $array[$i+1];
$spielerB = $array[$i+2];
}
}
fclose($datei);

// Spielordner löschen
if (is_dir("Games/".$gameid)) 
deleteDirectory("Games/".$gameid);

// noch aus Spieler Datei löschen
@$array = file("User/".rtrim($spielerA).".dat");
$datei = fopen("User/".rtrim($spielerA).".dat", "w");
for ($i=0;$i<count($array);$i++){
if (rtrim($array[$i])=="<games>"){
fwrite($datei,$array[$i]);
for ($b=$i+1;$b<count($array);$b++){
if (rtrim($array[$b])=="<endgames>"){
fwrite($datei,"<endgames>\r\n");
for ($c=$b+2;$c<count($array);$c++){
fwrite($datei,$array[$c]);
}
break;
}
if (rtrim($array[$b])!=$gameid)fwrite($datei,$array[$b]);
}
break;
}
fwrite($datei,$array[$i]);
}
fclose($datei);

if (rtrim($spielerB)!=""){
@$array = file("User/".rtrim($spielerB).".dat");
$datei = fopen("User/".rtrim($spielerB).".dat", "w");
for ($i=0;$i<count($array);$i++){
if (rtrim($array[$i])=="<games>"){
fwrite($datei,$array[$i]);
for ($b=$i+1;$b<count($array);$b++){
if (rtrim($array[$b])=="<endgames>"){
fwrite($datei,"<endgames>\r\n");
for ($c=$b+2;$c<count($array);$c++){
fwrite($datei,$array[$c]);
}
break;
}
if (rtrim($array[$b])!=$gameid)fwrite($datei,$array[$b]);
}
break;
}
fwrite($datei,$array[$i]);
}
fclose($datei);
}

echo "Spiel wurde gelöscht\r\n";
?>