<?php
include('function.php');
include('input.php');
include('check_input.php');
// name, passwd

// alle Spiele mit Nutzer entfernen
@$Darray = file("User/".$name.".dat");
for ($c=0;$c<count($Darray);$c++){
if (rtrim($Darray[$c])=="<games>"){

for ($b=$i+1;$b<count($Darray);$b++){
if (rtrim($Darray[$b])=="<endgames>")break;
// das löschen 
gameid = $Darray[$b];

///////////////////////////////////////////////
// offenes Spiel löschen
$dat2 = "OffeneSpiele.txt";
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

// Spiel löschen
$spielerA=$name;
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
else
{
$spielerA=$array[$i+1];
$spielerB=$array[$i+2];
}
}
fclose($datei);

///////////
@$array = file("User/".rtrim($spielerA).".dat");
$datei = fopen("User/".rtrim($spielerA).".dat", "w");
for ($d=0;$d<count($array);$d++){
if (rtrim($array[$d])=="<games>"){
fwrite($datei,$array[$d]);
for ($e=$d+1;$e<count($array);$e++){
if (rtrim($array[$e])=="<endgames>"){
fwrite($datei,"<endgames>\r\n");
for ($f=$e+2;$f<count($array);$f++){
fwrite($datei,$array[$f]);
}
break;
}
if (rtrim($array[$e])!=$gameid)fwrite($datei,$array[$e]);
}
break;
}
fwrite($datei,$array[$d]);
}
fclose($datei);

if (rtrim($spielerB)!=""){
@$array = file("User/".rtrim($spielerB).".dat");
$datei = fopen("User/".rtrim($spielerB).".dat", "w");
for ($d=0;$d<count($array);$d++){
if (rtrim($array[$d])=="<games>"){
fwrite($datei,$array[$d]);
for ($e=$d+1;$e<count($array);$e++){
if (rtrim($array[$e])=="<endgames>"){
fwrite($datei,"<endgames>\r\n");
for ($f=$e+2;$f<count($array);$f++){
fwrite($datei,$array[$f]);
}
break;
}
if (rtrim($array[$e])!=$gameid)fwrite($datei,$array[$e]);
}
break;
}
fwrite($datei,$array[$d]);
}
fclose($datei);
}
///////////

$dat = "User/".$name.".dat";
unlink($dat);

// Spielordner löschen
if (is_dir("Games/".$gameid)) 
deleteDirectory("Games/".$gameid);
///////////////////////////////////////////////


}
break;
}}


echo "Spieler wurde gelöscht\r\n"
?>