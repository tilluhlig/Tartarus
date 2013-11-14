<?php
include('input.php');
include('check_input.php');
include('check_gameid.php');
// name, passwd, gameid

$dat = "Games/".$gameid."/GameInfo.dat";
if (!file_exists($dat) ){
echo "FEHLER\r\nGameInfodatei existiert nicht!\r\n";
exit;
}

@$m_array = file($dat);
for ($i=0;$i<count($m_array);$i++){
echo $m_array[$i];
}
?>