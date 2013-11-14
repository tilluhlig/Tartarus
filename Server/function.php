<?php
//Delete folder function
function deleteDirectory($dir) {
    if (!file_exists($dir)) return true;
    if (!is_dir($dir) || is_link($dir)) return unlink($dir);
        foreach (scandir($dir) as $item) {
				if ($item == '.' || $item == '..') continue;
    
				if (true){
				}

            if (!deleteDirectory($dir . "/" . $item)) {
                chmod($dir . "/" . $item, 0777);
                if (!deleteDirectory($dir . "/" . $item)) return false;
            };
        }
        return rmdir($dir);
    }

		  function random_string($chars = 6)
{
  
  $s = uniqid(mt_rand(), true);
  $s = sha1($s);
  $s = substr($s, mt_rand(0, 40-$chars), $chars);
  
  return $s;
}
?> 