$(document).ready(function(){new Chartist.Line("#simple-line-chart",{labels:["Monday","Tuesday","Wednesday","Thursday","Friday"],series:[[12,9,7,8,5],[2,1,3.5,7,3],[1,3,4,5,6]]},{fullWidth:!0,chartPadding:{right:40}});var e=new Chartist.Line("#line-data-holes",{labels:[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16],series:[[5,5,10,8,7,5,4,null,null,null,10,10,7,8,6,9],[10,15,null,12,null,10,12,15,null,null,12,null,14,null,null,null],[null,null,null,null,3,4,1,3,4,6,7,9,5,null,null,null]]},{fullWidth:!0,chartPadding:{right:10},low:0}),e=new Chartist.Line("#line-data-fill-holes",{labels:[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16],series:[[5,5,10,8,7,5,4,null,null,null,10,10,7,8,6,9],[10,15,null,12,null,10,12,15,null,null,12,null,14,null,null,null],[null,null,null,null,3,4,1,3,4,6,7,9,5,null,null,null]]},{fullWidth:!0,chartPadding:{right:10},lineSmooth:Chartist.Interpolation.cardinal({fillHoles:!0}),low:0});new Chartist.Line("#line-only-integer",{labels:[1,2,3,4,5,6,7,8],series:[[1,2,3,1,-2,0,1,0],[-2,-1,-2,-1,-3,-1,-2,-1],[0,0,0,1,2,3,2,1],[3,2,1,.5,1,0,-1,-3]]},{high:3,low:-3,fullWidth:!0,axisY:{onlyInteger:!0,offset:20}});var n=function(e){return Array.apply(null,new Array(e))},a=n(52).map(Math.random).reduce(function(e,n,a){return e.labels.push(a+1),e.series.forEach(function(e){e.push(100*Math.random())}),e},{labels:[],series:n(4).map(function(){return new Array})}),t={showLine:!1,axisX:{labelInterpolationFnc:function(e,n){return n%13===0?"W"+e:null}}},i=[["screen and (min-width: 640px)",{axisX:{labelInterpolationFnc:function(e,n){return n%4===0?"W"+e:null}}}]];new Chartist.Line("#line-scatter-random",a,t,i),new Chartist.Line("#line-area",{labels:[1,2,3,4,5,6,7,8],series:[[5,9,7,8,5,3,5,4]]},{low:0,showArea:!0}),new Chartist.Line("#bipolar-line-area",{labels:[1,2,3,4,5,6,7,8],series:[[1,2,3,1,-2,0,1,0],[-2,-1,-2,-1,-2.5,-1,-2,-1],[0,0,0,1,2,2.5,2,1],[2.5,2,1,.5,1,.5,-1,-2.5]]},{high:3,low:-3,showArea:!0,showLine:!1,showPoint:!1,fullWidth:!0,axisX:{showLabel:!1,showGrid:!1}});var e=new Chartist.Line("#line-modify-drawing",{labels:[1,2,3,4,5],series:[[12,9,7,8,5]]});e.on("draw",function(e){if("point"===e.type){var n=new Chartist.Svg("path",{d:["M",e.x,e.y-15,"L",e.x-15,e.y+8,"L",e.x+15,e.y+8,"z"].join(" "),style:"fill-opacity: 1"},"ct-area");e.element.replace(n)}});var e=new Chartist.Line("#line-svg-animation",{labels:["1","2","3","4","5","6","7","8","9","10","11","12"],series:[[12,9,7,8,5,4,6,2,3,3,4,6],[4,5,3,7,3,5,5,3,4,4,5,5],[5,3,4,5,6,3,3,4,5,6,3,4],[3,4,5,6,7,6,4,5,6,7,6,3]]},{low:0}),r=0,s=80,l=500;e.on("created",function(){r=0}),e.on("draw",function(e){if(r++,"line"===e.type)e.element.animate({opacity:{begin:r*s+1e3,dur:l,from:0,to:1}});else if("label"===e.type&&"x"===e.axis)e.element.animate({y:{begin:r*s,dur:l,from:e.y+100,to:e.y,easing:"easeOutQuart"}});else if("label"===e.type&&"y"===e.axis)e.element.animate({x:{begin:r*s,dur:l,from:e.x-100,to:e.x,easing:"easeOutQuart"}});else if("point"===e.type)e.element.animate({x1:{begin:r*s,dur:l,from:e.x-10,to:e.x,easing:"easeOutQuart"},x2:{begin:r*s,dur:l,from:e.x-10,to:e.x,easing:"easeOutQuart"},opacity:{begin:r*s,dur:l,from:0,to:1,easing:"easeOutQuart"}});else if("grid"===e.type){var n={begin:r*s,dur:l,from:e[e.axis.units.pos+"1"]-30,to:e[e.axis.units.pos+"1"],easing:"easeOutQuart"},a={begin:r*s,dur:l,from:e[e.axis.units.pos+"2"]-100,to:e[e.axis.units.pos+"2"],easing:"easeOutQuart"},t={};t[e.axis.units.pos+"1"]=n,t[e.axis.units.pos+"2"]=a,t.opacity={begin:r*s,dur:l,from:0,to:1,easing:"easeOutQuart"},e.element.animate(t)}}),e.on("created",function(){window.__exampleAnimateTimeout&&(clearTimeout(window.__exampleAnimateTimeout),window.__exampleAnimateTimeout=null),window.__exampleAnimateTimeout=setTimeout(e.update.bind(e),12e3)});var e=new Chartist.Line("#line-path-animation",{labels:["Mon","Tue","Wed","Thu","Fri","Sat"],series:[[1,5,2,5,4,3],[2,3,4,8,1,2],[5,4,3,2,1,.5]]},{low:0,showArea:!0,showPoint:!1,fullWidth:!0});e.on("draw",function(e){("line"===e.type||"area"===e.type)&&e.element.animate({d:{begin:2e3*e.index,dur:2e3,from:e.path.clone().scale(1,0).translate(0,e.chartRect.height()).stringify(),to:e.path.clone().stringify(),easing:Chartist.Svg.Easing.easeOutQuint}})});var e=new Chartist.Line("#line-simple-smoothing",{labels:[1,2,3,4,5],series:[[1,5,10,0,1],[10,15,0,1,2]]},{lineSmooth:Chartist.Interpolation.simple({divisor:2}),fullWidth:!0,chartPadding:{right:20},low:0}),e=new Chartist.Line("#line-series-override",{labels:["1","2","3","4","5","6","7","8"],series:[{name:"series-1",data:[5,2,-4,2,0,-2,5,-3]},{name:"series-2",data:[4,3,5,3,1,3,6,4]},{name:"series-3",data:[2,4,3,1,4,5,3,2]}]},{fullWidth:!0,series:{"series-1":{lineSmooth:Chartist.Interpolation.step()},"series-2":{lineSmooth:Chartist.Interpolation.simple(),showArea:!0},"series-3":{showPoint:!1}}},[["screen and (max-width: 320px)",{series:{"series-1":{lineSmooth:Chartist.Interpolation.none()},"series-2":{lineSmooth:Chartist.Interpolation.none(),showArea:!1},"series-3":{lineSmooth:Chartist.Interpolation.none(),showPoint:!0}}}]]),a={labels:["W1","W2","W3","W4","W5","W6","W7","W8","W9","W10"],series:[[1,2,4,8,6,-2,-1,-4,-6,-2]]},t={high:10,low:-10,axisX:{labelInterpolationFnc:function(e,n){return n%2===0?e:null}}};new Chartist.Bar("#bi-polar-bar-interpolated",a,t);var a={labels:["Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"],series:[[5,4,3,7,5,10,3,4,8,10,6,8],[3,2,9,5,4,6,4,6,7,8,7,4]]},t={seriesBarDistance:10},i=[["screen and (max-width: 640px)",{seriesBarDistance:5,axisX:{labelInterpolationFnc:function(e){return e[0]}}}]];new Chartist.Bar("#overlapping-bars",a,t,i);var e=new Chartist.Bar("#bar-with-circle-modify-drawing",{labels:["W1","W2","W3","W4","W5","W6","W7","W8","W9","W10"],series:[[1,2,4,8,6,-2,-1,-4,-6,-2]]},{high:10,low:-10,axisX:{labelInterpolationFnc:function(e,n){return n%2===0?e:null}}});e.on("draw",function(e){"bar"===e.type&&e.group.append(new Chartist.Svg("circle",{cx:e.x2,cy:e.y2,r:2*Math.abs(Chartist.getMultiValue(e.value))+5},"ct-slice-pie"))}),new Chartist.Bar("#multiline-bar",{labels:["First quarter of the year","Second quarter of the year","Third quarter of the year","Fourth quarter of the year"],series:[[6e4,4e4,8e4,7e4],[4e4,3e4,7e4,65e3],[8e3,3e3,1e4,6e3]]},{seriesBarDistance:10,axisX:{offset:60},axisY:{offset:80,labelInterpolationFnc:function(e){return e+" CHF"},scaleMinSpace:15}}),new Chartist.Bar("#stacked-bar",{labels:["Q1","Q2","Q3","Q4"],series:[[8e5,12e5,14e5,13e5],[2e5,4e5,5e5,3e5],[1e5,2e5,4e5,6e5]]},{stackBars:!0,axisY:{labelInterpolationFnc:function(e){return e/1e3+"k"}}}).on("draw",function(e){"bar"===e.type&&e.element.attr({style:"stroke-width: 30px"})}),new Chartist.Bar("#bar-horizontal",{labels:["Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"],series:[[5,4,3,7,5,10,3],[3,2,9,5,4,6,4]]},{seriesBarDistance:10,reverseData:!0,horizontalBars:!0,axisY:{offset:70}}),new Chartist.Bar("#bar-extreme-responsive",{labels:["Quarter 1","Quarter 2","Quarter 3","Quarter 4"],series:[[5,4,3,7],[3,2,9,5],[1,5,8,4],[2,3,4,6],[4,1,2,1]]},{stackBars:!0,axisX:{labelInterpolationFnc:function(e){return e.split(/\s+/).map(function(e){return e[0]}).join("")}},axisY:{offset:20}},[["screen and (min-width: 400px)",{reverseData:!0,horizontalBars:!0,axisX:{labelInterpolationFnc:Chartist.noop},axisY:{offset:60}}],["screen and (min-width: 800px)",{stackBars:!1,seriesBarDistance:10}],["screen and (min-width: 1000px)",{reverseData:!1,horizontalBars:!1,seriesBarDistance:15}]]),new Chartist.Bar("#bar-distributed-series",{labels:["XS","S","M","L","XL","XXL","XXXL"],series:[20,60,120,200,180,20,10]},{distributeSeries:!0}),new Chartist.Bar("#bar-label-position",{labels:["Mon","Tue","Wed","Thu","Fri","Sat","Sun"],series:[[5,4,3,7,5,10,3],[3,2,9,5,4,6,4]]},{axisX:{position:"start"},axisY:{position:"end"}});var a={series:[5,3,4]},o=function(e,n){return e+n};new Chartist.Pie("#simple-pie-chart",a,{labelInterpolationFnc:function(e){return Math.round(e/a.series.reduce(o)*100)+"%"}});var a={labels:["Bananas","Apples","Grapes"],series:[20,15,40]},t={labelInterpolationFnc:function(e){return e[0]}},i=[["screen and (min-width: 640px)",{chartPadding:30,labelOffset:100,labelDirection:"explode",labelInterpolationFnc:function(e){return e}}],["screen and (min-width: 1024px)",{labelOffset:80,chartPadding:20}]];new Chartist.Pie("#pie-with-custom-labels",a,t,i),new Chartist.Pie("#simple-gauge-chart",{series:[20,10,30,40]},{donut:!0,donutWidth:60,startAngle:270,total:200,showLabel:!1});var e=new Chartist.Pie("#donut-animation",{series:[10,20,50,20,5,50,15],labels:[1,2,3,4,5,6,7]},{donut:!0,showLabel:!1});e.on("draw",function(e){if("slice"===e.type){var n=e.element._node.getTotalLength();e.element.attr({"stroke-dasharray":n+"px "+n+"px"});var a={"stroke-dashoffset":{id:"anim"+e.index,dur:1e3,from:-n+"px",to:"0px",easing:Chartist.Svg.Easing.easeOutQuint,fill:"freeze"}};0!==e.index&&(a["stroke-dashoffset"].begin="anim"+(e.index-1)+".end"),e.element.attr({"stroke-dashoffset":-n+"px"}),e.element.animate(a,!1)}}),e.on("created",function(){window.__anim21278907124&&(clearTimeout(window.__anim21278907124),window.__anim21278907124=null),window.__anim21278907124=setTimeout(e.update.bind(e),1e4)})});