<?php
include('function.php');
include('input.php');
include('check_input.php');
// name, passwd, version
$gameid=random_string(12);
for (;file_exists("Games/".$gameid."/GameInfo.dat");){
$gameid=random_string(12);
}

// Spielerdaten setzen
@$array = file("User/".$name.".dat");
$datei = fopen("User/".$name.".dat", "w");
for ($i=0;$i<count($array);$i++){
if (rtrim($array[$i])=="<games>"){
fwrite($datei,$array[$i]);
for ($b=$i+1;$b<count($array);$b=$b+1){
if (rtrim($array[$b])=="<endgames>"){
fwrite($datei,"$gameid\r\n");
fwrite($datei,"<endgames>\r\n");
for ($c=$b+2;$c<count($array);$c=$c+1){
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

// Spieldaten setzen
mkdir("Games/".$gameid, 0777);
chmod("Games/".$gameid, 0777);

$dat = "Games/".$gameid."/GameInfo.dat";
$datei = fopen($dat, "w"); 
fwrite($datei,"$name\r\n");
fwrite($datei,"$name\r\n");
fwrite($datei,"\r\n");
fwrite($datei,"$version\r\n");
fwrite($datei,"0\r\n");
fwrite($datei,"0\r\n");
fclose($datei);
chmod($dat, 0777);

$datei = fopen("OffeneSpiele.txt", "a");
fwrite($datei,"$gameid\r\n");
fwrite($datei,"$name\r\n");
fwrite($datei,"$version\r\n");
fclose($datei);

$datei = fopen("Spiele.txt", "a");
fwrite($datei,"$gameid\r\n");
fwrite($datei,"$name\r\n");
fwrite($datei,"\r\n");
fwrite($datei,"$version\r\n");
fclose($datei);

echo $gameid."\r\n";

?>