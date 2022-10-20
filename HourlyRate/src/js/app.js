/*
Template Name: ASPSTUDIO - Responsive Bootstrap 5 Admin Template
Version: 3.2.0
Author: Sean Ngu
	----------------------------
		APPS CONTENT TABLE
	----------------------------

	<!-- ======== GLOBAL SCRIPT SETTING ======== -->
  01. Global Variable
  02. Handle Scrollbar
  03. Handle Sidebar Menu
  04. Handle Sidebar Minify
  05. Handle Sidebar Minify Float Menu
  06. Handle Dropdown Close Option
  07. Handle Card - Remove / Reload / Collapse / Expand
  08. Handle Tooltip & Popover Activation
  09. Handle Scroll to Top Button Activation
  10. Handle hexToRgba
  11. Handle Scroll to
  12. Handle Theme Panel Expand
  13. Handle Theme Page Control
  14. Handle Enable Tooltip & Popover
	
	<!-- ======== APPLICATION SETTING ======== -->
	Application Controller
*/



/* 01. Global Variable
------------------------------------------------ */
var app = {
	class: 'app',
	isMobile: ((/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) || window.innerWidth < 992),
	header: {
		class: 'app-header'
	},
	sidebar: {
		class: 'app-sidebar',
		menuClass: 'menu',
		menuItemClass: 'menu-item',
		menuItemHasSubClass: 'has-sub',
		menuLinkClass: 'menu-link',
		menuSubmenuClass: 'menu-submenu',
		menuExpandClass: 'expand',
		minify: {
			toggledClass: 'app-sidebar-minified',
			localStorage: 'appSidebarMinified',
			toggleAttr: 'data-toggle="sidebar-minify"'
		},
		mobile: {
			toggledClass: 'app-sidebar-mobile-toggled',
			closedClass: 'app-sidebar-mobile-closed',
			dismissAttr: 'data-dismiss="sidebar-mobile"',
			toggleAttr: 'data-toggle="sidebar-mobile"',
		},
		scrollBar: {
			localStorage: 'appSidebarScrollPosition',
			dom: '',
		}
	},
	floatSubmenu: {
		id: 'app-float-submenu',
		dom: '',
		timeout: '',
		class: 'app-float-submenu',
		container: {
			class: 'app-float-submenu-container'
		},
		overflow: {
			class: 'overflow-scroll mh-100vh'
		}
	},
	themePanel: {
		class: 'theme-panel',
		toggleAttr: 'data-click="theme-panel-expand"',
		expandCookie: 'theme-panel',
		expandCookieValue: 'expand',
		activeClass: 'active',
		themeList: {
			class: 'theme-list',
			toggleAttr: 'data-theme',
			activeClass: 'active',
			cookieName: 'theme',
			onChangeEvent: 'theme-change'
		},
		darkMode: {
			class: 'dark-mode',
			inputName: 'app-theme-dark-mode',
			cookieName: 'dark-mode'
		}
	},
	animation: { 
		speed: 300 
	},
	scrollBar: {
		attr: 'data-scrollbar="true"',
		heightAttr: 'data-height',
		skipMobileAttr: 'data-skip-mobile="true"',
		wheelPropagationAttr: 'data-wheel-propagation'
	},
	scrollTo: {
		toggleAttr: 'data-toggle="scroll-to"',
		targetAttr: 'data-target'
	},
	scrollTopButton: {
		toggleAttr: 'data-click="scroll-top"',
		showClass: 'show'
	},
	card: { 
		class: 'card',
		expand: {
			toggleAttr: 'data-toggle="card-expand"',
			status: false,
			class: 'card-expand',
			tooltipText: 'Expand / Compress'
		}
	},
	tooltip: {
		toggleAttr: 'data-bs-toggle="tooltip"'
	},
	popover: {
		toggleAttr: 'data-bs-toggle="popover"'
	},
	font: { },
	color: { },
}

var slideUp = function(elm, duration = app.animation.speed) {
	if (!elm.classList.contains('transitioning')) {
		elm.classList.add('transitioning');
		elm.style.transitionProperty = 'height, margin, padding';
		elm.style.transitionDuration = duration + 'ms';
		elm.style.boxSizing = 'border-box';
		elm.style.height = elm.offsetHeight + 'px';
		elm.offsetHeight;
		elm.style.overflow = 'hidden';
		elm.style.height = 0;
		elm.style.paddingTop = 0;
		elm.style.paddingBottom = 0;
		elm.style.marginTop = 0;
		elm.style.marginBottom = 0;
		window.setTimeout( () => {
			elm.style.display = 'none';
			elm.style.removeProperty('height');
			elm.style.removeProperty('padding-top');
			elm.style.removeProperty('padding-bottom');
			elm.style.removeProperty('margin-top');
			elm.style.removeProperty('margin-bottom');
			elm.style.removeProperty('overflow');
			elm.style.removeProperty('transition-duration');
			elm.style.removeProperty('transition-property');
			elm.classList.remove('transitioning');
		}, duration);
	}
};

var slideDown = function(elm, duration = app.animation.speed) {
	if (!elm.classList.contains('transitioning')) {
		elm.classList.add('transitioning');
		elm.style.removeProperty('display');
		let display = window.getComputedStyle(elm).display;
		if (display === 'none') display = 'block';
		elm.style.display = display;
		let height = elm.offsetHeight;
		elm.style.overflow = 'hidden';
		elm.style.height = 0;
		elm.style.paddingTop = 0;
		elm.style.paddingBottom = 0;
		elm.style.marginTop = 0;
		elm.style.marginBottom = 0;
		elm.offsetHeight;
		elm.style.boxSizing = 'border-box';
		elm.style.transitionProperty = "height, margin, padding";
		elm.style.transitionDuration = duration + 'ms';
		elm.style.height = height + 'px';
		elm.style.removeProperty('padding-top');
		elm.style.removeProperty('padding-bottom');
		elm.style.removeProperty('margin-top');
		elm.style.removeProperty('margin-bottom');
		window.setTimeout( () => {
			elm.style.removeProperty('height');
			elm.style.removeProperty('overflow');
			elm.style.removeProperty('transition-duration');
			elm.style.removeProperty('transition-property');
			elm.classList.remove('transitioning');
		}, duration);
	}
};

var slideToggle = function(elm, duration = app.animation.speed) {
	if (window.getComputedStyle(elm).display === 'none') {
		return slideDown(elm, duration);
	} else {
		return slideUp(elm, duration);
	}
};

var setCookie = function(cookieName, cookieValue) {
	var now = new Date();
  var time = now.getTime();
  var expireTime = time + 1000*36000;
  now.setTime(expireTime);
  document.cookie = cookieName + '='+ cookieValue +';expires='+now.toUTCString()+';path=/';
};

var getCookie = function(cookieName) {
  let name = cookieName + "=";
  let decodedCookie = decodeURIComponent(document.cookie);
  let ca = decodedCookie.split(';');
  for(let i = 0; i <ca.length; i++) {
    let c = ca[i];
    while (c.charAt(0) == ' ') {
      c = c.substring(1);
    }
    if (c.indexOf(name) == 0) {
      return c.substring(name.length, c.length);
    }
  }
  return "";
};


/* 02. Handle Scrollbar
------------------------------------------------ */
var handleScrollbar = function() {
	"use strict";
	
	var elms = document.querySelectorAll('['+ app.scrollBar.attr +']');
		
	for (var i = 0; i < elms.length; i++) {
		generateScrollbar(elms[i])
	}
};
var generateScrollbar = function(elm) {
  "use strict";
	
	if (elm.scrollbarInit || (app.isMobile && elm.getAttribute(app.scrollBar.skipMobileAttr))) {
		return;
	}
	var dataHeight = (!elm.getAttribute(app.scrollBar.heightAttr)) ? elm.offsetHeight : elm.getAttribute(app.scrollBar.heightAttr);
	
	elm.style.height = dataHeight;
	elm.scrollbarInit = true;
	
	if(app.isMobile || !PerfectScrollbar) {
		elm.style.overflowX = 'scroll';
	} else {
		var dataWheelPropagation = (elm.getAttribute(app.scrollBar.wheelPropagationAttr)) ? elm.getAttribute(app.scrollBar.wheelPropagationAttr) : false;
		
		if (PerfectScrollbar) {
			if (elm.closest('.'+ app.sidebar.class )) {
				app.sidebar.scrollBarDom = new PerfectScrollbar(elm, {
					wheelPropagation: dataWheelPropagation
				});
			} else {
				new PerfectScrollbar(elm, {
					wheelPropagation: dataWheelPropagation
				});
			}
		}
	}
};


/* 03. Handle Sidebar Menu
------------------------------------------------ */
var handleSidebarMenuToggle = function(menus) {
	menus.map(function(menu) {
		menu.onclick = function(e) {
			e.preventDefault();
			
			var target = this.nextElementSibling;
			
			if (!document.querySelector('.'+ app.sidebar.minify.toggledClass)) {
				slideToggle(target);
				
				menus.map(function(m) {
					var otherTarget = m.nextElementSibling;
					if (otherTarget !== target) {
						slideUp(otherTarget);
						otherTarget.closest('.'+ app.sidebar.menuItemClass).classList.remove(app.sidebar.menuExpandClass);
					}
				});
				
				var targetElm = target.closest('.'+ app.sidebar.menuItemClass);
				if (targetElm.classList.contains(app.sidebar.menuExpandClass)) {
					targetElm.classList.remove(app.sidebar.menuExpandClass);
				} else {
					targetElm.classList.add(app.sidebar.menuExpandClass);
				}
			}
		}
	});
};
var handleSidebarMenu = function() {
	"use strict";
	
	var menus = [].slice.call(document.querySelectorAll('.'+ app.sidebar.class +' .'+ app.sidebar.menuClass +' > .'+ app.sidebar.menuItemClass +'.'+ app.sidebar.menuItemHasSubClass +' > .'+ app.sidebar.menuLinkClass +''));
	handleSidebarMenuToggle(menus);
	
	var menus = [].slice.call(document.querySelectorAll('.'+ app.sidebar.class +' .'+ app.sidebar.menuClass +' > .'+ app.sidebar.menuItemClass +'.'+ app.sidebar.menuItemHasSubClass +' .'+ app.sidebar.menuSubmenuClass +' .'+ app.sidebar.menuItemClass +'.'+ app.sidebar.menuItemHasSubClass +' > .'+ app.sidebar.menuLinkClass +''));
	handleSidebarMenuToggle(menus);
};


/* 04. Handle Sidebar Scroll Memory
------------------------------------------------ */
var handleSidebarScrollMemory = function() {
	if (!app.isMobile) {
		try {
			if (typeof(Storage) !== 'undefined' && typeof(localStorage) !== 'undefined') {
				var elm = document.querySelector('.'+ app.sidebar.class +' ['+ app.scrollBar.attr +']');
				
				if (elm) {
					elm.onscroll = function() {
						localStorage.setItem(app.sidebar.scrollBar.localStorage, this.scrollTop);
					}
					var defaultScroll = localStorage.getItem(app.sidebar.scrollBar.localStorage);
					if (defaultScroll) {
						document.querySelector('.'+ app.sidebar.class +' ['+ app.scrollBar.attr +']').scrollTop = defaultScroll;
					}
				}
			}
		} catch (error) {
			console.log(error);
		}
	}
};


/* 04. Handle Sidebar Minify
------------------------------------------------ */
var handleSidebarMinify = function() {
	var elms = [].slice.call(document.querySelectorAll('['+ app.sidebar.minify.toggleAttr +']'));
	elms.map(function(elm) {
		elm.onclick = function(e) {
			e.preventDefault();
		
			var targetElm = document.querySelector('.'+ app.class);
			
			if (targetElm) {
				if (targetElm.classList.contains(app.sidebar.minify.toggledClass)) {
					targetElm.classList.remove(app.sidebar.minify.toggledClass);
					localStorage.removeItem(app.sidebar.minify.localStorage);
				} else {
					targetElm.classList.add(app.sidebar.minify.toggledClass);
					localStorage.setItem(app.sidebar.minify.localStorage, true);
				}
			}
		};
	});
	
	if (typeof(Storage) !== 'undefined') {
		if (localStorage[app.sidebar.minify.localStorage]) {
			var targetElm = document.querySelector('.'+ app.class);
			
			if (targetElm) {
				targetElm.classList.add(app.sidebar.minify.toggledClass);
			}
		}
	}
};
var handleSidebarMobileToggle = function() {
	var elms = [].slice.call(document.querySelectorAll('['+ app.sidebar.mobile.toggleAttr +']'));
	
	elms.map(function(elm) {
		elm.onclick = function(e) {
			e.preventDefault();
			
			var targetElm = document.querySelector('.'+ app.class)
			
			if (targetElm) {
				targetElm.classList.remove(app.sidebar.mobile.closedClass);
				targetElm.classList.add(app.sidebar.mobile.toggledClass);
			}
		};
	});
};
var handleSidebarMobileDismiss = function() {
	var elms = [].slice.call(document.querySelectorAll('['+ app.sidebar.mobile.dismissAttr +']'));
	
	elms.map(function(elm) {
		elm.onclick = function(e) {
			e.preventDefault();
			
			var targetElm = document.querySelector('.'+ app.class)
			
			if (targetElm) {
				targetElm.classList.remove(app.sidebar.mobile.toggledClass);
				targetElm.classList.add(app.sidebar.mobile.closedClass);
				
				setTimeout(function() {
					targetElm.classList.remove(app.sidebar.mobile.closedClass);
				}, app.animation.speed);
			}
		};
	});
};


/* 05. Handle Sidebar Minify Float Menu
------------------------------------------------ */
var handleGetHiddenMenuHeight = function(elm) {
	elm.setAttribute('style', 'position: absolute; visibility: hidden; display: block !important');
	var targetHeight  = elm.clientHeight;
	elm.removeAttribute('style');
	return targetHeight;
}
var handleSidebarMinifyFloatMenuClick = function() {
	var elms = [].slice.call(document.querySelectorAll('.'+ app.floatSubmenu.class +' .'+ app.sidebar.menuItemClass +'.'+ app.sidebar.menuItemHasSubClass +' > .'+ app.sidebar.menuLinkClass));
	if (elms) {
		elms.map(function(elm) {
			elm.onclick = function(e) {
				e.preventDefault();
				var targetItem = this.closest('.' + app.sidebar.menuItemClass);
				var target = targetItem.querySelector('.' + app.sidebar.menuSubmenuClass);
				var targetStyle = getComputedStyle(target);
				var close = (targetStyle.getPropertyValue('display') != 'none') ? true : false;
				var expand = (targetStyle.getPropertyValue('display') != 'none') ? false : true;
				
				slideToggle(target);
				
				var loopHeight = setInterval(function() {
					var targetMenu = document.querySelector(app.floatSubmenu.id);
					var targetMenuArrow = document.querySelector(app.floatSubmenu.arrow.id);
					var targetMenuLine = document.querySelector(app.floatSubmenu.line.id);
					var targetHeight = targetMenu.clientHeight;
					var targetOffset = targetMenu.getBoundingClientRect();
					var targetOriTop = targetMenu.getAttribute('data-offset-top');
					var targetMenuTop = targetMenu.getAttribute('data-menu-offset-top');
					var targetTop 	 = targetOffset.top;
					var windowHeight = document.body.clientHeight;
					if (close) {
						if (targetTop > targetOriTop) {
							targetTop = (targetTop > targetOriTop) ? targetOriTop : targetTop;
							targetMenu.style.top = targetTop + 'px';
							targetMenu.style.bottom = 'auto';
							targetMenuArrow.style.top = '20px';
							targetMenuArrow.style.bottom = 'auto';
							targetMenuLine.style.top = '20px';
							targetMenuLine.style.bottom = 'auto';
						}
					}
					if (expand) {
						if ((windowHeight - targetTop) < targetHeight) {
							var arrowBottom = (windowHeight - targetMenuTop) - 22;
							targetMenu.style.top = 'auto';
							targetMenu.style.bottom = 0;
							targetMenuArrow.style.top = 'auto';
							targetMenuArrow.style.bottom = arrowBottom + 'px';
							targetMenuLine.style.top = '20px';
							targetMenuLine.style.bottom = arrowBottom + 'px';
						}
						var floatSubmenuElm = document.querySelector(app.floatSubmenu.id + ' .'+ app.floatSubmenu.class);
						if (targetHeight > windowHeight) {
							if (floatSubmenuElm) {
								var splitClass = (app.floatSubmenu.overflow.class).split(' ');
								for (var i = 0; i < splitClass.length; i++) {
									floatSubmenuElm.classList.add(splitClass[i]);
								}
							}
						}
					}
				}, 1);
				setTimeout(function() {
					clearInterval(loopHeight);
				}, app.animation.speed);
			}
		});
	}
}
var handleSidebarMinifyFloatMenu = function() {
	var elms = [].slice.call(document.querySelectorAll('.' + app.sidebar.class + ' .'+ app.sidebar.menuClass +' > .'+ app.sidebar.menuItemClass +'.'+ app.sidebar.menuItemHasSubClass +' > .'+ app.sidebar.menuLinkClass +''));
	if (elms) {
		elms.map(function(elm) {
			elm.onmouseenter = function() {
				var appElm = document.querySelector('.' + app.class);
				
				if (appElm && appElm.classList.contains(app.sidebar.minify.toggledClass)) {
					clearTimeout(app.floatSubmenu.timeout);
					
					var targetMenu = this.closest('.'+ app.sidebar.menuItemClass).querySelector('.' + app.sidebar.menuSubmenuClass);
					if (app.floatSubmenu.dom == this && document.querySelector(app.floatSubmenu.class)) {
						return;
					} else {
						app.floatSubmenu.dom = this;
					}
					var targetMenuHtml = targetMenu.innerHTML;
					if (targetMenuHtml) {
						var bodyStyle     = getComputedStyle(document.body);
						var sidebarOffset = document.querySelector('.'+ app.sidebar.class).getBoundingClientRect();
						var sidebarWidth  = parseInt(document.querySelector('.'+ app.sidebar.class).clientWidth);
						var sidebarX      = (bodyStyle.getPropertyValue('direction') != 'rtl') ? (sidebarOffset.left + sidebarWidth) : (document.body.clientWidth - sidebarOffset.left);
						var targetHeight  = handleGetHiddenMenuHeight(targetMenu);
						var targetOffset  = this.getBoundingClientRect();
						var targetTop     = targetOffset.top;
						var targetLeft    = (bodyStyle.getPropertyValue('direction') != 'rtl') ? sidebarX : 'auto';
						var targetRight   = (bodyStyle.getPropertyValue('direction') != 'rtl') ? 'auto' : sidebarX;
						var windowHeight  = document.body.clientHeight;
						
						if (!document.querySelector('#'+ app.floatSubmenu.id)) {
							var overflowClass = '';
							if (targetHeight > windowHeight) {
								overflowClass = app.floatSubmenu.overflow.class;
							}
							var html = document.createElement('div');
							html.setAttribute('id', app.floatSubmenu.id);
							html.setAttribute('class', app.floatSubmenu.class);
							html.setAttribute('data-offset-top', targetTop);
							html.setAttribute('data-menu-offset-top', targetTop);
							html.innerHTML = ''+
							'	<div class="'+ app.floatSubmenu.container.class +' '+ overflowClass +'">'+ targetMenuHtml + '</div>';
							appElm.appendChild(html);
							
							var elm = document.getElementById(app.floatSubmenu.id);
							elm.onmouseover = function() {
								clearTimeout(app.floatSubmenu.timeout);
							};
							elm.onmouseout = function() {
								app.floatSubmenu.timeout = setTimeout(() => {
									document.querySelector('#'+ app.floatSubmenu.id).remove();
								}, app.animation.speed);
							};
						} else {
							var floatSubmenu = document.querySelector('#'+ app.floatSubmenu.id);
							var floatSubmenuElm = document.querySelector('#'+ app.floatSubmenu.id + ' .'+ app.floatSubmenu.container.class);
							
							if (targetHeight > windowHeight) {
								if (floatSubmenuElm) {
									var splitClass = (app.floatSubmenu.overflow.class).split(' ');
									for (var i = 0; i < splitClass.length; i++) {
										floatSubmenuElm.classList.add(splitClass[i]);
									}
								}
							}
							floatSubmenu.setAttribute('data-offset-top', targetTop);
							floatSubmenu.setAttribute('data-menu-offset-top', targetTop);
							floatSubmenuElm.innerHTML = targetMenuHtml;
						}
				
						var targetHeight = document.querySelector('#'+ app.floatSubmenu.id).clientHeight;
						var floatSubmenuElm = document.querySelector('#'+ app.floatSubmenu.id);
						if ((windowHeight - targetTop) > targetHeight) {
							if (floatSubmenuElm) {
								floatSubmenuElm.style.top = targetTop + 'px';
								floatSubmenuElm.style.left = targetLeft + 'px';
								floatSubmenuElm.style.bottom = 'auto';
								floatSubmenuElm.style.right = targetRight + 'px';
							}
						} else {
							var arrowBottom = (windowHeight - targetTop) - 21;
							if (floatSubmenuElm) {
								floatSubmenuElm.style.top = 'auto';
								floatSubmenuElm.style.left = targetLeft + 'px';
								floatSubmenuElm.style.bottom = 0;
								floatSubmenuElm.style.right = targetRight + 'px';
							}
						}
						handleSidebarMinifyFloatMenuClick();
					} else {
						document.querySelector('#'+ app.floatSubmenu.id).remove();
						app.floatSubmenu.dom = '';
					}
				}
			}
			elm.onmouseleave = function() {
				var elm = document.querySelector('.' + app.class);
				if (elm && elm.classList.contains(app.sidebar.minify.toggledClass)) {
					app.floatSubmenu.timeout = setTimeout(() => {
						document.querySelector('#'+ app.floatSubmenu.id).remove();
						app.floatSubmenu.dom = '';
					}, 250);
				}
			}
		});
	}
};


/* 07. Handle Card - Expand
------------------------------------------------ */
var handleCardAction = function() {
	"use strict";

	if (app.card.expand.status) {
		return false;
	}
	app.card.expandStatus = true;

	// expand
	var expandTogglerList = [].slice.call(document.querySelectorAll('['+ app.card.expand.toggleAttr +']'));
	var expandTogglerTooltipList = expandTogglerList.map(function (expandTogglerEl) {
		expandTogglerEl.onclick = function(e) {
			e.preventDefault();
		
			var target = this.closest('.'+ app.card.class);
			var targetClass = app.card.expand.class;

			if (document.body.classList.contains(targetClass) && target.classList.contains(targetClass)) {
				target.removeAttribute('style');
				target.classList.remove(targetClass);
				document.body.classList.remove(targetClass);
			} else {
				document.body.classList.add(targetClass);
				target.classList.add(targetClass);
			}
		
			window.dispatchEvent(new Event('resize'));
		};
	
		return new bootstrap.Tooltip(expandTogglerEl, {
			title: app.card.expand.tooltipText,
			placement: 'bottom',
			trigger: 'hover',
			container: 'body'
		});
	});
};


/* 08. Handle Tooltip & Popover Activation
------------------------------------------------ */
var handelTooltipPopoverActivation = function() {
	"use strict";
	var tooltipTriggerList = [].slice.call(document.querySelectorAll('['+ app.tooltip.toggleAttr +']'))
	var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
		return new bootstrap.Tooltip(tooltipTriggerEl);
	});
	
	var popoverTriggerList = [].slice.call(document.querySelectorAll('['+ app.popover.toggleAttr +']'))
	var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
		return new bootstrap.Popover(popoverTriggerEl);
	});
};


/* 09. Handle Scroll to Top Button Activation
------------------------------------------------ */
var handleScrollToTopButton = function() {
	"use strict";
	var elmTriggerList = [].slice.call(document.querySelectorAll('['+ app.scrollTopButton.toggleAttr +']'));
	
	document.onscroll = function() {
		var doc = document.documentElement;
		var totalScroll = (window.pageYOffset || doc.scrollTop)  - (doc.clientTop || 0);
		var elmList = elmTriggerList.map(function(elm) {
			if (totalScroll >= 200) {
				if (!elm.classList.contains(app.scrollTopButton.showClass)) {
					elm.classList.add(app.scrollTopButton.showClass);
				}
			} else {
				elm.classList.remove(app.scrollTopButton.showClass);
			}
		});
	}
	
	var elmList = elmTriggerList.map(function(elm) {
		elm.onclick = function(e) {
			e.preventDefault();
			
			window.scrollTo({top: 0, behavior: 'smooth'});
		}
	});
};


/* 11. Handle Scroll to
------------------------------------------------ */
var handleScrollTo = function() {
	var elmTriggerList = [].slice.call(document.querySelectorAll('['+ app.scrollTo.toggleAttr +']'));
	var elmList = elmTriggerList.map(function(elm) {
		elm.onclick = function(e) {
			e.preventDefault();
		
			var targetAttr = (elm.getAttribute(app.scrollTo.targetAttr)) ? this.getAttribute(app.scrollTo.targetAttr) : this.getAttribute('href');
			var targetElm = document.querySelectorAll(targetAttr)[0];
			var targetHeader = document.querySelectorAll('.'+ app.header.class)[0];
			var targetHeight = targetHeader.offsetHeight;
			if (targetElm) {
				var targetTop = targetElm.offsetTop - targetHeight + 36;
				window.scrollTo({top: targetTop, behavior: 'smooth'});
			}
		}
	});
};


/* 12. Handle Theme Panel Expand
------------------------------------------------ */
var handleThemePanelExpand = function() {
	var elmList = [].slice.call(document.querySelectorAll('['+ app.themePanel.toggleAttr +']'));
	
	elmList.map(function(elm) {
		elm.onclick = function(e) {
			e.preventDefault();
			
			var targetContainer = document.querySelector('.'+ app.themePanel.class);
			var targetExpand = false;
		
			if (targetContainer.classList.contains(app.themePanel.activeClass)) {
				targetContainer.classList.remove(app.themePanel.activeClass);
				setCookie(app.themePanel.expandCookie, '');
			} else {
				targetContainer.classList.add(app.themePanel.activeClass);
				setCookie(app.themePanel.expandCookie, app.themePanel.expandCookieValue);
			}
		}
	});
	
	if (getCookie(app.themePanel.expandCookie) && getCookie(app.themePanel.expandCookie) == app.themePanel.expandCookieValue) {
		var elm = document.querySelector('['+ app.themePanel.toggleAttr +']');
		if (elm) {
			elm.click();
		}
	}
};


/* 13. Handle Theme Page Control
------------------------------------------------ */
var handleThemePageControl = function() {
	// Theme Click
	var elms = [].slice.call(document.querySelectorAll('.'+ app.themePanel.themeList.class +' ['+ app.themePanel.themeList.toggleAttr +']'));
	elms.map(function(elm) {
		elm.onclick = function() {
			var targetThemeClass = this.getAttribute(app.themePanel.themeList.toggleAttr);
			for (var x = 0; x < document.body.classList.length; x++) {
				var targetClass = document.body.classList[x];
				if (targetClass.search('theme-') > -1) {
					document.body.classList.remove(targetClass);
				}
			}
			if (targetThemeClass) {
				document.body.classList.add(targetThemeClass);
			}
		
			var togglers = [].slice.call(document.querySelectorAll('.'+ app.themePanel.themeList.class +' ['+ app.themePanel.themeList.toggleAttr +']'));
			togglers.map(function(toggler) {
				if (toggler != elm) {
					toggler.closest('li').classList.remove(app.themePanel.themeList.activeClass);
				} else {
					toggler.closest('li').classList.add(app.themePanel.themeList.activeClass);
				}
			});
			setCookie(app.themePanel.themeList.cookieName, targetThemeClass);
			document.dispatchEvent(new CustomEvent(app.themePanel.themeList.onChangeEvent));
		}
	});
	
	// Theme Cookie
	if (getCookie(app.themePanel.themeList.cookieName) && document.querySelector('.'+ app.themePanel.themeList.class)) {
		var targetElm = document.querySelector('.'+ app.themePanel.themeList.class +' ['+ app.themePanel.themeList.toggleAttr +'="'+ getCookie(app.themePanel.themeList.cookieName) +'"]');
		if (targetElm) {
			targetElm.click();
		}
	}
	
	// Dark Mode Click
	var elms = [].slice.call(document.querySelectorAll('.'+ app.themePanel.class +' [name="'+ app.themePanel.darkMode.inputName +'"]'));
	elms.map(function(elm) {
		elm.onchange = function() {
			var targetCookie = '';
	
			if (this.checked) {
				document.querySelector('html').classList.add(app.themePanel.darkMode.class);
				targetCookie = 'dark-mode';
			} else {
				document.querySelector('html').classList.remove(app.themePanel.darkMode.class);
			}
			App.initVariable();
			setCookie(app.themePanel.darkMode.cookieName, targetCookie);
			document.dispatchEvent(new CustomEvent(app.themePanel.themeList.onChangeEvent));
		}
	});
	
	// Dark Mode Cookie
	if (getCookie(app.themePanel.darkMode.cookieName) && document.querySelector('.'+ app.themePanel.class +' [name="'+ app.themePanel.darkMode.inputName +'"]')) {
		var elm = document.querySelector('.'+ app.themePanel.class +' [name="'+ app.themePanel.darkMode.inputName +'"]');
		if (elm) {
			elm.checked = true;
			elm.onchange();
		}
	}
};


/* Application Controller
------------------------------------------------ */
var App = function () {
	"use strict";
	
	return {
		//main function
		init: function () {
			this.initComponent();
			this.initVariable();
			this.initHeader();
			this.initSidebar();
		},
		initSidebar: function() {
			handleSidebarScrollMemory();
			handleSidebarMinifyFloatMenu();
			handleSidebarMenu();
			handleSidebarMinify();
			handleSidebarMobileToggle();
			handleSidebarMobileDismiss();
		},
		initHeader: function() {
		},
		initComponent: function() {
			handleScrollbar();
			handleCardAction();
			handelTooltipPopoverActivation();
			handleScrollToTopButton();
			handleScrollTo();
			handleThemePanelExpand();
			handleThemePageControl();
		},
		scrollTop: function() {
			window.scrollTo({top: 0, behavior: 'smooth'});
		},
		getCssVariable: function(variable) {
			return window.getComputedStyle(document.body).getPropertyValue(variable).trim();
		},
		initVariable: function() {
			app.color.theme          = this.getCssVariable('--app-theme');
			app.font.family          = this.getCssVariable('--bs-body-font-family');
			app.font.size            = this.getCssVariable('--bs-body-font-size');
			app.font.weight          = this.getCssVariable('--bs-body-font-weight');
			app.color.componentColor = this.getCssVariable('--app-component-color');
			app.color.componentBg    = this.getCssVariable('--app-component-bg');
			app.color.dark           = this.getCssVariable('--bs-dark');
			app.color.light          = this.getCssVariable('--bs-light');
			app.color.blue           = this.getCssVariable('--bs-blue');
			app.color.indigo         = this.getCssVariable('--bs-indigo');
			app.color.purple         = this.getCssVariable('--bs-purple');
			app.color.pink           = this.getCssVariable('--bs-pink');
			app.color.red            = this.getCssVariable('--bs-red');
			app.color.orange         = this.getCssVariable('--bs-orange');
			app.color.yellow         = this.getCssVariable('--bs-yellow');
			app.color.green          = this.getCssVariable('--bs-green');
			app.color.success        = this.getCssVariable('--bs-success');
			app.color.teal           = this.getCssVariable('--bs-teal');
			app.color.cyan           = this.getCssVariable('--bs-cyan');
			app.color.white          = this.getCssVariable('--bs-white');
			app.color.gray           = this.getCssVariable('--bs-gray');
			app.color.lime           = this.getCssVariable('--bs-lime');
			app.color.gray100        = this.getCssVariable('--bs-gray-100');
			app.color.gray200        = this.getCssVariable('--bs-gray-200');
			app.color.gray300        = this.getCssVariable('--bs-gray-300');
			app.color.gray400        = this.getCssVariable('--bs-gray-400');
			app.color.gray500        = this.getCssVariable('--bs-gray-500');
			app.color.gray600        = this.getCssVariable('--bs-gray-600');
			app.color.gray700        = this.getCssVariable('--bs-gray-700');
			app.color.gray800        = this.getCssVariable('--bs-gray-800');
			app.color.gray900        = this.getCssVariable('--bs-gray-900');
			app.color.black          = this.getCssVariable('--bs-black');
			
			app.color.themeRgb          = this.getCssVariable('--app-theme-rgb');
			app.font.familyRgb          = this.getCssVariable('--bs-body-font-family-rgb');
			app.font.sizeRgb            = this.getCssVariable('--bs-body-font-size-rgb');
			app.font.weightRgb          = this.getCssVariable('--bs-body-font-weight-rgb');
			app.color.componentColorRgb = this.getCssVariable('--app-component-color-rgb');
			app.color.componentBgRgb    = this.getCssVariable('--app-component-bg-rgb');
			app.color.darkRgb           = this.getCssVariable('--bs-dark-rgb');
			app.color.lightRgb          = this.getCssVariable('--bs-light-rgb');
			app.color.blueRgb           = this.getCssVariable('--bs-blue-rgb');
			app.color.indigoRgb         = this.getCssVariable('--bs-indigo-rgb');
			app.color.purpleRgb         = this.getCssVariable('--bs-purple-rgb');
			app.color.pinkRgb           = this.getCssVariable('--bs-pink-rgb');
			app.color.redRgb            = this.getCssVariable('--bs-red-rgb');
			app.color.orangeRgb         = this.getCssVariable('--bs-orange-rgb');
			app.color.yellowRgb         = this.getCssVariable('--bs-yellow-rgb');
			app.color.greenRgb          = this.getCssVariable('--bs-green-rgb');
			app.color.successRgb        = this.getCssVariable('--bs-success-rgb');
			app.color.tealRgb           = this.getCssVariable('--bs-teal-rgb');
			app.color.cyanRgb           = this.getCssVariable('--bs-cyan-rgb');
			app.color.whiteRgb          = this.getCssVariable('--bs-white-rgb');
			app.color.grayRgb           = this.getCssVariable('--bs-gray-rgb');
			app.color.limeRgb           = this.getCssVariable('--bs-lime-rgb');
			app.color.gray100Rgb        = this.getCssVariable('--bs-gray-100-rgb');
			app.color.gray200Rgb        = this.getCssVariable('--bs-gray-200-rgb');
			app.color.gray300Rgb        = this.getCssVariable('--bs-gray-300-rgb');
			app.color.gray400Rgb        = this.getCssVariable('--bs-gray-400-rgb');
			app.color.gray500Rgb        = this.getCssVariable('--bs-gray-500-rgb');
			app.color.gray600Rgb        = this.getCssVariable('--bs-gray-600-rgb');
			app.color.gray700Rgb        = this.getCssVariable('--bs-gray-700-rgb');
			app.color.gray800Rgb        = this.getCssVariable('--bs-gray-800-rgb');
			app.color.gray900Rgb        = this.getCssVariable('--bs-gray-900-rgb');
			app.color.blackRgb          = this.getCssVariable('--bs-black-rgb');
		}
	};
}();

document.addEventListener('DOMContentLoaded', function() {
	App.init();
});