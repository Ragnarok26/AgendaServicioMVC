/*
jQWidgets v4.5.0 (2017-Jan)
Copyright (c) 2011-2017 jQWidgets.
License: http://jqwidgets.com/license/
*/
!function(a){a.extend(a.jqx._jqxGrid.prototype,{_calculateaggregate:function(b,c,d,e){var f=b.aggregates;if(f||(f=c),f){for(var g=new Array,h=0;h<f.length;h++)"count"!=f[h]&&(g[g.length]=b.cellsformat);if(this.source&&this.source.getAggregatedData){if(void 0==e&&(e=this.getrows()),this.virtualmode){var e=new Array;a.each(this.source._source.records,function(){e.push(this)})}if(void 0==d||1==d){var i=this.source.getAggregatedData([{name:b.datafield,aggregates:f,formatStrings:g}],this.gridlocalization,e);return i}var i=this.source.getAggregatedData([{name:b.datafield,aggregates:f}],this.gridlocalization,e);return i}}return null},getcolumnaggregateddata:function(a,b,c,d){var e=this.getcolumn(a),f=void 0!=c&&0!=c&&c;if(null==b)return"";var g=e.aggregates;e.aggregates=null;var h=this._calculateaggregate(e,b,f,d),i={};return h&&(i=h[a]),e.aggregates=g,i},refreshaggregates:function(){this._updatecolumnsaggregates()},renderaggregates:function(){this._updateaggregates()},_updatecolumnaggregates:function(b,c,d){var e=this;if(c)if(d.children().remove(),d.html(""),b.aggregatesrenderer){if(c){var f=b.aggregatesrenderer(c[b.datafield],b,d,this.getcolumnaggregateddata(b.datafield,c[b.datafield]));d.html(f)}}else a.each(c,function(){var b=this;for(g in b){var c=a('<div style="position: relative; margin: 4px; overflow: hidden;"></div>'),f=g;f=e._getaggregatename(f),c.html(f+":"+b[g]),e.rtl&&c.addClass(e.toThemeProperty("jqx-rtl")),d.append(c)}});else if(d.children().remove(),d.html(""),b.aggregatesrenderer){var g={};b.aggregates&&(g=this.getcolumnaggregateddata(b.datafield,b.aggregates));var f=b.aggregatesrenderer({},b,d,null);d.html(f)}},_getaggregatetype:function(a){switch(a){case"min":case"max":case"count":case"avg":case"product":case"var":case"varp":case"stdev":case"stdevp":case"sum":return a}var b=a;for(var c in a){b=c;break}return b},_getaggregatename:function(a){var b=a;switch(a){case"min":b="Min";break;case"max":b="Max";break;case"count":b="Count";break;case"avg":b="Avg";break;case"product":b="Product";break;case"var":b="Var";break;case"stdevp":b="StDevP";break;case"stdev":b="StDev";break;case"varp":b="VarP";case"sum":b="Sum"}if(a===b&&"string"!=typeof b)for(var c in a){b=c;break}return b},_updatecolumnsaggregates:function(){var b=this.getrows(),c=this.columns.records.length;if(void 0!=this.statusbar[0].cells)for(var d=0;d<c;d++){var e=a(this.statusbar[0].cells[d]),f=this.columns.records[d],g=this._calculateaggregate(f,null,!0,b);this._updatecolumnaggregates(f,g,e)}},_updateaggregates:function(){var b=a('<div style="position: relative;" id="statusrow'+this.element.id+'"></div>'),c=0,d=this.columns.records.length,e=this.toThemeProperty("jqx-grid-cell");this.rtl&&(e+=" "+this.toThemeProperty("jqx-grid-cell-rtl"),c=-1),e+=" "+this.toThemeProperty("jqx-grid-cell-pinned");var f=d+10,g=new Array;this.statusbar[0].cells=g;for(var h=this.getrows(),i=0;i<d;i++){var j=this.columns.records[i],k=this._calculateaggregate(j,j.aggregates,!0,h),l=j.width;l<j.minwidth&&(l=j.minwidth),l>j.maxwidth&&(l=j.maxwidth);var m=e;j.cellsalign&&(m+=" "+this.toThemeProperty("jqx-"+j.cellsalign+"-align"));var n=a('<div style="overflow: hidden; position: absolute; height: 100%;" class="'+m+'"></div>');b.append(n),n.css("left",c),this.rtl?n.css("z-index",f++):n.css("z-index",f--),n.width(l),n[0].left=c,j.hidden&&j.hideable?n.css("display","none"):c+=l,g[g.length]=n[0],this._updatecolumnaggregates(j,k,n)}a.jqx.browser.msie&&a.jqx.browser.version<8&&b.css("z-index",f--),b.width(parseFloat(c)+2),b.height(this.statusbarheight),this.statusbar.children().remove(),this.statusbar.append(b),this.statusbar.removeClass(this.toThemeProperty("jqx-widget-header")),this.statusbar.addClass(e),this.statusbar.css("border-bottom-color","transparent"),this.statusbar.css("border-top-width","1px"),this.rtl&&"hidden"!=this.hScrollBar.css("visibility")&&this._renderhorizontalscroll(),this._arrange()}})}(jqxBaseFramework);

