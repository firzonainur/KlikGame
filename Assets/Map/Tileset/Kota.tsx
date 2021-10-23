<?xml version="1.0" encoding="UTF-8"?>
<tileset version="1.5" tiledversion="1.7.2" name="Kota" tilewidth="32" tileheight="32" tilecount="154" columns="14">
 <image source="modern-city-assets/outdoors.png" width="448" height="352"/>
 <tile id="10">
  <objectgroup draworder="index" id="2">
   <object id="1" x="23.375" y="8.25" height="0.125" visible="0"/>
   <object id="2" x="0" y="0" width="32" visible="0"/>
   <object id="3" x="23.0909" y="8.63636" height="0.0909091" visible="0"/>
   <object id="4" x="24.0909" y="8.18182" width="2" height="3.36364" visible="0"/>
   <object id="5" x="15.3636" y="-0.363636" width="16.6364" height="15.9091" visible="0"/>
   <object id="6" x="0" y="-1" width="31" height="15.75" visible="0"/>
  </objectgroup>
 </tile>
 <tile id="12">
  <objectgroup draworder="index" id="3">
   <object id="6" x="0" y="0" width="32" height="32"/>
  </objectgroup>
 </tile>
 <wangsets>
  <wangset name="Tets" type="mixed" tile="60">
   <wangcolor name="Grass" color="#ff0000" tile="60" probability="1"/>
   <wangcolor name="Statue" color="#00ff00" tile="-1" probability="1"/>
   <wangtile tileid="60" wangid="2,2,1,2,1,1,1,1"/>
  </wangset>
 </wangsets>
</tileset>
