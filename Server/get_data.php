<?php
include('input.php');
include('check_input.php');
include('check_gameid.php');
// name, passwd, gameid

$dat2 = "Games/".$gameid."/Data.dat";
if (!file_exists($dat2) ){
echo "FEHLER\r\nData-Datei existiert nicht!\r\n";
exit;
}

$dat = "Games/".$gameid."/GameInfo.dat";
if (!file_exists($dat) ){
echo "FEHLER\r\nGameInfodatei existiert nicht!\r\n";
exit;
}

$dat = "Games/".$gameid."/GameInfo.dat";
@$m_array = file($dat);
if (rtrim($m_array[0]) != $name){
echo "FEHLER\r\nSpieler ist nicht am Zug\r\n";
exit;
}

if (rtrim($m_array[5])=="0"){
$m_array[5]="1\r\n";
$datei = fopen($dat, "w");
for ($i=0;$i<count($m_array);$i++){
fwrite($datei,$m_array[$i]);
}
fclose($datei);

@$m_array = file($dat2);
for ($i=0;$i<count($m_array);$i++){
echo $m_array[$i];
}
}
else{
echo "FEHLER\r\nData-datei bereits runtergeladen!\r\n";
exit;
}


?>