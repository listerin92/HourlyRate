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
//# sourceMappingURL=data:application/json;charset=utf8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImFwcC5qcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBIiwiZmlsZSI6ImFwcC5taW4uanMiLCJzb3VyY2VzQ29udGVudCI6WyIvKlxuVGVtcGxhdGUgTmFtZTogU1RVRElPIC0gUmVzcG9uc2l2ZSBCb290c3RyYXAgNSBBZG1pbiBUZW1wbGF0ZVxuVmVyc2lvbjogMy4xLjBcbkF1dGhvcjogU2VhbiBOZ3Vcblx0LS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxuXHRcdEFQUFMgQ09OVEVOVCBUQUJMRVxuXHQtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXG5cblx0PCEtLSA9PT09PT09PSBHTE9CQUwgU0NSSVBUIFNFVFRJTkcgPT09PT09PT0gLS0+XG4gIDAxLiBHbG9iYWwgVmFyaWFibGVcbiAgMDIuIEhhbmRsZSBTY3JvbGxiYXJcbiAgMDMuIEhhbmRsZSBTaWRlYmFyIE1lbnVcbiAgMDQuIEhhbmRsZSBTaWRlYmFyIE1pbmlmeVxuICAwNS4gSGFuZGxlIFNpZGViYXIgTWluaWZ5IEZsb2F0IE1lbnVcbiAgMDYuIEhhbmRsZSBEcm9wZG93biBDbG9zZSBPcHRpb25cbiAgMDcuIEhhbmRsZSBDYXJkIC0gUmVtb3ZlIC8gUmVsb2FkIC8gQ29sbGFwc2UgLyBFeHBhbmRcbiAgMDguIEhhbmRsZSBUb29sdGlwICYgUG9wb3ZlciBBY3RpdmF0aW9uXG4gIDA5LiBIYW5kbGUgU2Nyb2xsIHRvIFRvcCBCdXR0b24gQWN0aXZhdGlvblxuICAxMC4gSGFuZGxlIGhleFRvUmdiYVxuICAxMS4gSGFuZGxlIFNjcm9sbCB0b1xuICAxMi4gSGFuZGxlIFRoZW1lIFBhbmVsIEV4cGFuZFxuICAxMy4gSGFuZGxlIFRoZW1lIFBhZ2UgQ29udHJvbFxuICAxNC4gSGFuZGxlIEVuYWJsZSBUb29sdGlwICYgUG9wb3ZlclxuXHRcblx0PCEtLSA9PT09PT09PSBBUFBMSUNBVElPTiBTRVRUSU5HID09PT09PT09IC0tPlxuXHRBcHBsaWNhdGlvbiBDb250cm9sbGVyXG4qL1xuXG5cblxuLyogMDEuIEdsb2JhbCBWYXJpYWJsZVxuLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tICovXG52YXIgYXBwID0ge1xuXHRjbGFzczogJ2FwcCcsXG5cdGlzTW9iaWxlOiAoKC9BbmRyb2lkfHdlYk9TfGlQaG9uZXxpUGFkfGlQb2R8QmxhY2tCZXJyeXxJRU1vYmlsZXxPcGVyYSBNaW5pL2kudGVzdChuYXZpZ2F0b3IudXNlckFnZW50KSkgfHwgd2luZG93LmlubmVyV2lkdGggPCA5OTIpLFxuXHRoZWFkZXI6IHtcblx0XHRjbGFzczogJ2FwcC1oZWFkZXInXG5cdH0sXG5cdHNpZGViYXI6IHtcblx0XHRjbGFzczogJ2FwcC1zaWRlYmFyJyxcblx0XHRtZW51Q2xhc3M6ICdtZW51Jyxcblx0XHRtZW51SXRlbUNsYXNzOiAnbWVudS1pdGVtJyxcblx0XHRtZW51SXRlbUhhc1N1YkNsYXNzOiAnaGFzLXN1YicsXG5cdFx0bWVudUxpbmtDbGFzczogJ21lbnUtbGluaycsXG5cdFx0bWVudVN1Ym1lbnVDbGFzczogJ21lbnUtc3VibWVudScsXG5cdFx0bWVudUV4cGFuZENsYXNzOiAnZXhwYW5kJyxcblx0XHRtaW5pZnk6IHtcblx0XHRcdHRvZ2dsZWRDbGFzczogJ2FwcC1zaWRlYmFyLW1pbmlmaWVkJyxcblx0XHRcdGxvY2FsU3RvcmFnZTogJ2FwcFNpZGViYXJNaW5pZmllZCcsXG5cdFx0XHR0b2dnbGVBdHRyOiAnZGF0YS10b2dnbGU9XCJzaWRlYmFyLW1pbmlmeVwiJ1xuXHRcdH0sXG5cdFx0bW9iaWxlOiB7XG5cdFx0XHR0b2dnbGVkQ2xhc3M6ICdhcHAtc2lkZWJhci1tb2JpbGUtdG9nZ2xlZCcsXG5cdFx0XHRjbG9zZWRDbGFzczogJ2FwcC1zaWRlYmFyLW1vYmlsZS1jbG9zZWQnLFxuXHRcdFx0ZGlzbWlzc0F0dHI6ICdkYXRhLWRpc21pc3M9XCJzaWRlYmFyLW1vYmlsZVwiJyxcblx0XHRcdHRvZ2dsZUF0dHI6ICdkYXRhLXRvZ2dsZT1cInNpZGViYXItbW9iaWxlXCInLFxuXHRcdH0sXG5cdFx0c2Nyb2xsQmFyOiB7XG5cdFx0XHRsb2NhbFN0b3JhZ2U6ICdhcHBTaWRlYmFyU2Nyb2xsUG9zaXRpb24nLFxuXHRcdFx0ZG9tOiAnJyxcblx0XHR9XG5cdH0sXG5cdGZsb2F0U3VibWVudToge1xuXHRcdGlkOiAnYXBwLWZsb2F0LXN1Ym1lbnUnLFxuXHRcdGRvbTogJycsXG5cdFx0dGltZW91dDogJycsXG5cdFx0Y2xhc3M6ICdhcHAtZmxvYXQtc3VibWVudScsXG5cdFx0Y29udGFpbmVyOiB7XG5cdFx0XHRjbGFzczogJ2FwcC1mbG9hdC1zdWJtZW51LWNvbnRhaW5lcidcblx0XHR9LFxuXHRcdG92ZXJmbG93OiB7XG5cdFx0XHRjbGFzczogJ292ZXJmbG93LXNjcm9sbCBtaC0xMDB2aCdcblx0XHR9XG5cdH0sXG5cdHRoZW1lUGFuZWw6IHtcblx0XHRjbGFzczogJ3RoZW1lLXBhbmVsJyxcblx0XHR0b2dnbGVBdHRyOiAnZGF0YS1jbGljaz1cInRoZW1lLXBhbmVsLWV4cGFuZFwiJyxcblx0XHRleHBhbmRDb29raWU6ICd0aGVtZS1wYW5lbCcsXG5cdFx0ZXhwYW5kQ29va2llVmFsdWU6ICdleHBhbmQnLFxuXHRcdGFjdGl2ZUNsYXNzOiAnYWN0aXZlJyxcblx0XHR0aGVtZUxpc3Q6IHtcblx0XHRcdGNsYXNzOiAndGhlbWUtbGlzdCcsXG5cdFx0XHR0b2dnbGVBdHRyOiAnZGF0YS10aGVtZScsXG5cdFx0XHRhY3RpdmVDbGFzczogJ2FjdGl2ZScsXG5cdFx0XHRjb29raWVOYW1lOiAndGhlbWUnLFxuXHRcdFx0b25DaGFuZ2VFdmVudDogJ3RoZW1lLWNoYW5nZSdcblx0XHR9LFxuXHRcdGRhcmtNb2RlOiB7XG5cdFx0XHRjbGFzczogJ2RhcmstbW9kZScsXG5cdFx0XHRpbnB1dE5hbWU6ICdhcHAtdGhlbWUtZGFyay1tb2RlJyxcblx0XHRcdGNvb2tpZU5hbWU6ICdkYXJrLW1vZGUnXG5cdFx0fVxuXHR9LFxuXHRhbmltYXRpb246IHsgXG5cdFx0c3BlZWQ6IDMwMCBcblx0fSxcblx0c2Nyb2xsQmFyOiB7XG5cdFx0YXR0cjogJ2RhdGEtc2Nyb2xsYmFyPVwidHJ1ZVwiJyxcblx0XHRoZWlnaHRBdHRyOiAnZGF0YS1oZWlnaHQnLFxuXHRcdHNraXBNb2JpbGVBdHRyOiAnZGF0YS1za2lwLW1vYmlsZT1cInRydWVcIicsXG5cdFx0d2hlZWxQcm9wYWdhdGlvbkF0dHI6ICdkYXRhLXdoZWVsLXByb3BhZ2F0aW9uJ1xuXHR9LFxuXHRzY3JvbGxUbzoge1xuXHRcdHRvZ2dsZUF0dHI6ICdkYXRhLXRvZ2dsZT1cInNjcm9sbC10b1wiJyxcblx0XHR0YXJnZXRBdHRyOiAnZGF0YS10YXJnZXQnXG5cdH0sXG5cdHNjcm9sbFRvcEJ1dHRvbjoge1xuXHRcdHRvZ2dsZUF0dHI6ICdkYXRhLWNsaWNrPVwic2Nyb2xsLXRvcFwiJyxcblx0XHRzaG93Q2xhc3M6ICdzaG93J1xuXHR9LFxuXHRjYXJkOiB7IFxuXHRcdGNsYXNzOiAnY2FyZCcsXG5cdFx0ZXhwYW5kOiB7XG5cdFx0XHR0b2dnbGVBdHRyOiAnZGF0YS10b2dnbGU9XCJjYXJkLWV4cGFuZFwiJyxcblx0XHRcdHN0YXR1czogZmFsc2UsXG5cdFx0XHRjbGFzczogJ2NhcmQtZXhwYW5kJyxcblx0XHRcdHRvb2x0aXBUZXh0OiAnRXhwYW5kIC8gQ29tcHJlc3MnXG5cdFx0fVxuXHR9LFxuXHR0b29sdGlwOiB7XG5cdFx0dG9nZ2xlQXR0cjogJ2RhdGEtYnMtdG9nZ2xlPVwidG9vbHRpcFwiJ1xuXHR9LFxuXHRwb3BvdmVyOiB7XG5cdFx0dG9nZ2xlQXR0cjogJ2RhdGEtYnMtdG9nZ2xlPVwicG9wb3ZlclwiJ1xuXHR9LFxuXHRmb250OiB7IH0sXG5cdGNvbG9yOiB7IH0sXG59XG5cbnZhciBzbGlkZVVwID0gZnVuY3Rpb24oZWxtLCBkdXJhdGlvbiA9IGFwcC5hbmltYXRpb24uc3BlZWQpIHtcblx0aWYgKCFlbG0uY2xhc3NMaXN0LmNvbnRhaW5zKCd0cmFuc2l0aW9uaW5nJykpIHtcblx0XHRlbG0uY2xhc3NMaXN0LmFkZCgndHJhbnNpdGlvbmluZycpO1xuXHRcdGVsbS5zdHlsZS50cmFuc2l0aW9uUHJvcGVydHkgPSAnaGVpZ2h0LCBtYXJnaW4sIHBhZGRpbmcnO1xuXHRcdGVsbS5zdHlsZS50cmFuc2l0aW9uRHVyYXRpb24gPSBkdXJhdGlvbiArICdtcyc7XG5cdFx0ZWxtLnN0eWxlLmJveFNpemluZyA9ICdib3JkZXItYm94Jztcblx0XHRlbG0uc3R5bGUuaGVpZ2h0ID0gZWxtLm9mZnNldEhlaWdodCArICdweCc7XG5cdFx0ZWxtLm9mZnNldEhlaWdodDtcblx0XHRlbG0uc3R5bGUub3ZlcmZsb3cgPSAnaGlkZGVuJztcblx0XHRlbG0uc3R5bGUuaGVpZ2h0ID0gMDtcblx0XHRlbG0uc3R5bGUucGFkZGluZ1RvcCA9IDA7XG5cdFx0ZWxtLnN0eWxlLnBhZGRpbmdCb3R0b20gPSAwO1xuXHRcdGVsbS5zdHlsZS5tYXJnaW5Ub3AgPSAwO1xuXHRcdGVsbS5zdHlsZS5tYXJnaW5Cb3R0b20gPSAwO1xuXHRcdHdpbmRvdy5zZXRUaW1lb3V0KCAoKSA9PiB7XG5cdFx0XHRlbG0uc3R5bGUuZGlzcGxheSA9ICdub25lJztcblx0XHRcdGVsbS5zdHlsZS5yZW1vdmVQcm9wZXJ0eSgnaGVpZ2h0Jyk7XG5cdFx0XHRlbG0uc3R5bGUucmVtb3ZlUHJvcGVydHkoJ3BhZGRpbmctdG9wJyk7XG5cdFx0XHRlbG0uc3R5bGUucmVtb3ZlUHJvcGVydHkoJ3BhZGRpbmctYm90dG9tJyk7XG5cdFx0XHRlbG0uc3R5bGUucmVtb3ZlUHJvcGVydHkoJ21hcmdpbi10b3AnKTtcblx0XHRcdGVsbS5zdHlsZS5yZW1vdmVQcm9wZXJ0eSgnbWFyZ2luLWJvdHRvbScpO1xuXHRcdFx0ZWxtLnN0eWxlLnJlbW92ZVByb3BlcnR5KCdvdmVyZmxvdycpO1xuXHRcdFx0ZWxtLnN0eWxlLnJlbW92ZVByb3BlcnR5KCd0cmFuc2l0aW9uLWR1cmF0aW9uJyk7XG5cdFx0XHRlbG0uc3R5bGUucmVtb3ZlUHJvcGVydHkoJ3RyYW5zaXRpb24tcHJvcGVydHknKTtcblx0XHRcdGVsbS5jbGFzc0xpc3QucmVtb3ZlKCd0cmFuc2l0aW9uaW5nJyk7XG5cdFx0fSwgZHVyYXRpb24pO1xuXHR9XG59O1xuXG52YXIgc2xpZGVEb3duID0gZnVuY3Rpb24oZWxtLCBkdXJhdGlvbiA9IGFwcC5hbmltYXRpb24uc3BlZWQpIHtcblx0aWYgKCFlbG0uY2xhc3NMaXN0LmNvbnRhaW5zKCd0cmFuc2l0aW9uaW5nJykpIHtcblx0XHRlbG0uY2xhc3NMaXN0LmFkZCgndHJhbnNpdGlvbmluZycpO1xuXHRcdGVsbS5zdHlsZS5yZW1vdmVQcm9wZXJ0eSgnZGlzcGxheScpO1xuXHRcdGxldCBkaXNwbGF5ID0gd2luZG93LmdldENvbXB1dGVkU3R5bGUoZWxtKS5kaXNwbGF5O1xuXHRcdGlmIChkaXNwbGF5ID09PSAnbm9uZScpIGRpc3BsYXkgPSAnYmxvY2snO1xuXHRcdGVsbS5zdHlsZS5kaXNwbGF5ID0gZGlzcGxheTtcblx0XHRsZXQgaGVpZ2h0ID0gZWxtLm9mZnNldEhlaWdodDtcblx0XHRlbG0uc3R5bGUub3ZlcmZsb3cgPSAnaGlkZGVuJztcblx0XHRlbG0uc3R5bGUuaGVpZ2h0ID0gMDtcblx0XHRlbG0uc3R5bGUucGFkZGluZ1RvcCA9IDA7XG5cdFx0ZWxtLnN0eWxlLnBhZGRpbmdCb3R0b20gPSAwO1xuXHRcdGVsbS5zdHlsZS5tYXJnaW5Ub3AgPSAwO1xuXHRcdGVsbS5zdHlsZS5tYXJnaW5Cb3R0b20gPSAwO1xuXHRcdGVsbS5vZmZzZXRIZWlnaHQ7XG5cdFx0ZWxtLnN0eWxlLmJveFNpemluZyA9ICdib3JkZXItYm94Jztcblx0XHRlbG0uc3R5bGUudHJhbnNpdGlvblByb3BlcnR5ID0gXCJoZWlnaHQsIG1hcmdpbiwgcGFkZGluZ1wiO1xuXHRcdGVsbS5zdHlsZS50cmFuc2l0aW9uRHVyYXRpb24gPSBkdXJhdGlvbiArICdtcyc7XG5cdFx0ZWxtLnN0eWxlLmhlaWdodCA9IGhlaWdodCArICdweCc7XG5cdFx0ZWxtLnN0eWxlLnJlbW92ZVByb3BlcnR5KCdwYWRkaW5nLXRvcCcpO1xuXHRcdGVsbS5zdHlsZS5yZW1vdmVQcm9wZXJ0eSgncGFkZGluZy1ib3R0b20nKTtcblx0XHRlbG0uc3R5bGUucmVtb3ZlUHJvcGVydHkoJ21hcmdpbi10b3AnKTtcblx0XHRlbG0uc3R5bGUucmVtb3ZlUHJvcGVydHkoJ21hcmdpbi1ib3R0b20nKTtcblx0XHR3aW5kb3cuc2V0VGltZW91dCggKCkgPT4ge1xuXHRcdFx0ZWxtLnN0eWxlLnJlbW92ZVByb3BlcnR5KCdoZWlnaHQnKTtcblx0XHRcdGVsbS5zdHlsZS5yZW1vdmVQcm9wZXJ0eSgnb3ZlcmZsb3cnKTtcblx0XHRcdGVsbS5zdHlsZS5yZW1vdmVQcm9wZXJ0eSgndHJhbnNpdGlvbi1kdXJhdGlvbicpO1xuXHRcdFx0ZWxtLnN0eWxlLnJlbW92ZVByb3BlcnR5KCd0cmFuc2l0aW9uLXByb3BlcnR5Jyk7XG5cdFx0XHRlbG0uY2xhc3NMaXN0LnJlbW92ZSgndHJhbnNpdGlvbmluZycpO1xuXHRcdH0sIGR1cmF0aW9uKTtcblx0fVxufTtcblxudmFyIHNsaWRlVG9nZ2xlID0gZnVuY3Rpb24oZWxtLCBkdXJhdGlvbiA9IGFwcC5hbmltYXRpb24uc3BlZWQpIHtcblx0aWYgKHdpbmRvdy5nZXRDb21wdXRlZFN0eWxlKGVsbSkuZGlzcGxheSA9PT0gJ25vbmUnKSB7XG5cdFx0cmV0dXJuIHNsaWRlRG93bihlbG0sIGR1cmF0aW9uKTtcblx0fSBlbHNlIHtcblx0XHRyZXR1cm4gc2xpZGVVcChlbG0sIGR1cmF0aW9uKTtcblx0fVxufTtcblxudmFyIHNldENvb2tpZSA9IGZ1bmN0aW9uKGNvb2tpZU5hbWUsIGNvb2tpZVZhbHVlKSB7XG5cdHZhciBub3cgPSBuZXcgRGF0ZSgpO1xuICB2YXIgdGltZSA9IG5vdy5nZXRUaW1lKCk7XG4gIHZhciBleHBpcmVUaW1lID0gdGltZSArIDEwMDAqMzYwMDA7XG4gIG5vdy5zZXRUaW1lKGV4cGlyZVRpbWUpO1xuICBkb2N1bWVudC5jb29raWUgPSBjb29raWVOYW1lICsgJz0nKyBjb29raWVWYWx1ZSArJztleHBpcmVzPScrbm93LnRvVVRDU3RyaW5nKCkrJztwYXRoPS8nO1xufTtcblxudmFyIGdldENvb2tpZSA9IGZ1bmN0aW9uKGNvb2tpZU5hbWUpIHtcbiAgbGV0IG5hbWUgPSBjb29raWVOYW1lICsgXCI9XCI7XG4gIGxldCBkZWNvZGVkQ29va2llID0gZGVjb2RlVVJJQ29tcG9uZW50KGRvY3VtZW50LmNvb2tpZSk7XG4gIGxldCBjYSA9IGRlY29kZWRDb29raWUuc3BsaXQoJzsnKTtcbiAgZm9yKGxldCBpID0gMDsgaSA8Y2EubGVuZ3RoOyBpKyspIHtcbiAgICBsZXQgYyA9IGNhW2ldO1xuICAgIHdoaWxlIChjLmNoYXJBdCgwKSA9PSAnICcpIHtcbiAgICAgIGMgPSBjLnN1YnN0cmluZygxKTtcbiAgICB9XG4gICAgaWYgKGMuaW5kZXhPZihuYW1lKSA9PSAwKSB7XG4gICAgICByZXR1cm4gYy5zdWJzdHJpbmcobmFtZS5sZW5ndGgsIGMubGVuZ3RoKTtcbiAgICB9XG4gIH1cbiAgcmV0dXJuIFwiXCI7XG59O1xuXG5cbi8qIDAyLiBIYW5kbGUgU2Nyb2xsYmFyXG4tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0gKi9cbnZhciBoYW5kbGVTY3JvbGxiYXIgPSBmdW5jdGlvbigpIHtcblx0XCJ1c2Ugc3RyaWN0XCI7XG5cdFxuXHR2YXIgZWxtcyA9IGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3JBbGwoJ1snKyBhcHAuc2Nyb2xsQmFyLmF0dHIgKyddJyk7XG5cdFx0XG5cdGZvciAodmFyIGkgPSAwOyBpIDwgZWxtcy5sZW5ndGg7IGkrKykge1xuXHRcdGdlbmVyYXRlU2Nyb2xsYmFyKGVsbXNbaV0pXG5cdH1cbn07XG52YXIgZ2VuZXJhdGVTY3JvbGxiYXIgPSBmdW5jdGlvbihlbG0pIHtcbiAgXCJ1c2Ugc3RyaWN0XCI7XG5cdFxuXHRpZiAoZWxtLnNjcm9sbGJhckluaXQgfHwgKGFwcC5pc01vYmlsZSAmJiBlbG0uZ2V0QXR0cmlidXRlKGFwcC5zY3JvbGxCYXIuc2tpcE1vYmlsZUF0dHIpKSkge1xuXHRcdHJldHVybjtcblx0fVxuXHR2YXIgZGF0YUhlaWdodCA9ICghZWxtLmdldEF0dHJpYnV0ZShhcHAuc2Nyb2xsQmFyLmhlaWdodEF0dHIpKSA/IGVsbS5vZmZzZXRIZWlnaHQgOiBlbG0uZ2V0QXR0cmlidXRlKGFwcC5zY3JvbGxCYXIuaGVpZ2h0QXR0cik7XG5cdFxuXHRlbG0uc3R5bGUuaGVpZ2h0ID0gZGF0YUhlaWdodDtcblx0ZWxtLnNjcm9sbGJhckluaXQgPSB0cnVlO1xuXHRcblx0aWYoYXBwLmlzTW9iaWxlIHx8ICFQZXJmZWN0U2Nyb2xsYmFyKSB7XG5cdFx0ZWxtLnN0eWxlLm92ZXJmbG93WCA9ICdzY3JvbGwnO1xuXHR9IGVsc2Uge1xuXHRcdHZhciBkYXRhV2hlZWxQcm9wYWdhdGlvbiA9IChlbG0uZ2V0QXR0cmlidXRlKGFwcC5zY3JvbGxCYXIud2hlZWxQcm9wYWdhdGlvbkF0dHIpKSA/IGVsbS5nZXRBdHRyaWJ1dGUoYXBwLnNjcm9sbEJhci53aGVlbFByb3BhZ2F0aW9uQXR0cikgOiBmYWxzZTtcblx0XHRcblx0XHRpZiAoUGVyZmVjdFNjcm9sbGJhcikge1xuXHRcdFx0aWYgKGVsbS5jbG9zZXN0KCcuJysgYXBwLnNpZGViYXIuY2xhc3MgKSkge1xuXHRcdFx0XHRhcHAuc2lkZWJhci5zY3JvbGxCYXJEb20gPSBuZXcgUGVyZmVjdFNjcm9sbGJhcihlbG0sIHtcblx0XHRcdFx0XHR3aGVlbFByb3BhZ2F0aW9uOiBkYXRhV2hlZWxQcm9wYWdhdGlvblxuXHRcdFx0XHR9KTtcblx0XHRcdH0gZWxzZSB7XG5cdFx0XHRcdG5ldyBQZXJmZWN0U2Nyb2xsYmFyKGVsbSwge1xuXHRcdFx0XHRcdHdoZWVsUHJvcGFnYXRpb246IGRhdGFXaGVlbFByb3BhZ2F0aW9uXG5cdFx0XHRcdH0pO1xuXHRcdFx0fVxuXHRcdH1cblx0fVxufTtcblxuXG4vKiAwMy4gSGFuZGxlIFNpZGViYXIgTWVudVxuLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tICovXG52YXIgaGFuZGxlU2lkZWJhck1lbnVUb2dnbGUgPSBmdW5jdGlvbihtZW51cykge1xuXHRtZW51cy5tYXAoZnVuY3Rpb24obWVudSkge1xuXHRcdG1lbnUub25jbGljayA9IGZ1bmN0aW9uKGUpIHtcblx0XHRcdGUucHJldmVudERlZmF1bHQoKTtcblx0XHRcdFxuXHRcdFx0dmFyIHRhcmdldCA9IHRoaXMubmV4dEVsZW1lbnRTaWJsaW5nO1xuXHRcdFx0XG5cdFx0XHRpZiAoIWRvY3VtZW50LnF1ZXJ5U2VsZWN0b3IoJy4nKyBhcHAuc2lkZWJhci5taW5pZnkudG9nZ2xlZENsYXNzKSkge1xuXHRcdFx0XHRzbGlkZVRvZ2dsZSh0YXJnZXQpO1xuXHRcdFx0XHRcblx0XHRcdFx0bWVudXMubWFwKGZ1bmN0aW9uKG0pIHtcblx0XHRcdFx0XHR2YXIgb3RoZXJUYXJnZXQgPSBtLm5leHRFbGVtZW50U2libGluZztcblx0XHRcdFx0XHRpZiAob3RoZXJUYXJnZXQgIT09IHRhcmdldCkge1xuXHRcdFx0XHRcdFx0c2xpZGVVcChvdGhlclRhcmdldCk7XG5cdFx0XHRcdFx0XHRvdGhlclRhcmdldC5jbG9zZXN0KCcuJysgYXBwLnNpZGViYXIubWVudUl0ZW1DbGFzcykuY2xhc3NMaXN0LnJlbW92ZShhcHAuc2lkZWJhci5tZW51RXhwYW5kQ2xhc3MpO1xuXHRcdFx0XHRcdH1cblx0XHRcdFx0fSk7XG5cdFx0XHRcdFxuXHRcdFx0XHR2YXIgdGFyZ2V0RWxtID0gdGFyZ2V0LmNsb3Nlc3QoJy4nKyBhcHAuc2lkZWJhci5tZW51SXRlbUNsYXNzKTtcblx0XHRcdFx0aWYgKHRhcmdldEVsbS5jbGFzc0xpc3QuY29udGFpbnMoYXBwLnNpZGViYXIubWVudUV4cGFuZENsYXNzKSkge1xuXHRcdFx0XHRcdHRhcmdldEVsbS5jbGFzc0xpc3QucmVtb3ZlKGFwcC5zaWRlYmFyLm1lbnVFeHBhbmRDbGFzcyk7XG5cdFx0XHRcdH0gZWxzZSB7XG5cdFx0XHRcdFx0dGFyZ2V0RWxtLmNsYXNzTGlzdC5hZGQoYXBwLnNpZGViYXIubWVudUV4cGFuZENsYXNzKTtcblx0XHRcdFx0fVxuXHRcdFx0fVxuXHRcdH1cblx0fSk7XG59O1xudmFyIGhhbmRsZVNpZGViYXJNZW51ID0gZnVuY3Rpb24oKSB7XG5cdFwidXNlIHN0cmljdFwiO1xuXHRcblx0dmFyIG1lbnVzID0gW10uc2xpY2UuY2FsbChkb2N1bWVudC5xdWVyeVNlbGVjdG9yQWxsKCcuJysgYXBwLnNpZGViYXIuY2xhc3MgKycgLicrIGFwcC5zaWRlYmFyLm1lbnVDbGFzcyArJyA+IC4nKyBhcHAuc2lkZWJhci5tZW51SXRlbUNsYXNzICsnLicrIGFwcC5zaWRlYmFyLm1lbnVJdGVtSGFzU3ViQ2xhc3MgKycgPiAuJysgYXBwLnNpZGViYXIubWVudUxpbmtDbGFzcyArJycpKTtcblx0aGFuZGxlU2lkZWJhck1lbnVUb2dnbGUobWVudXMpO1xuXHRcblx0dmFyIG1lbnVzID0gW10uc2xpY2UuY2FsbChkb2N1bWVudC5xdWVyeVNlbGVjdG9yQWxsKCcuJysgYXBwLnNpZGViYXIuY2xhc3MgKycgLicrIGFwcC5zaWRlYmFyLm1lbnVDbGFzcyArJyA+IC4nKyBhcHAuc2lkZWJhci5tZW51SXRlbUNsYXNzICsnLicrIGFwcC5zaWRlYmFyLm1lbnVJdGVtSGFzU3ViQ2xhc3MgKycgLicrIGFwcC5zaWRlYmFyLm1lbnVTdWJtZW51Q2xhc3MgKycgLicrIGFwcC5zaWRlYmFyLm1lbnVJdGVtQ2xhc3MgKycuJysgYXBwLnNpZGViYXIubWVudUl0ZW1IYXNTdWJDbGFzcyArJyA+IC4nKyBhcHAuc2lkZWJhci5tZW51TGlua0NsYXNzICsnJykpO1xuXHRoYW5kbGVTaWRlYmFyTWVudVRvZ2dsZShtZW51cyk7XG59O1xuXG5cbi8qIDA0LiBIYW5kbGUgU2lkZWJhciBTY3JvbGwgTWVtb3J5XG4tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0gKi9cbnZhciBoYW5kbGVTaWRlYmFyU2Nyb2xsTWVtb3J5ID0gZnVuY3Rpb24oKSB7XG5cdGlmICghYXBwLmlzTW9iaWxlKSB7XG5cdFx0dHJ5IHtcblx0XHRcdGlmICh0eXBlb2YoU3RvcmFnZSkgIT09ICd1bmRlZmluZWQnICYmIHR5cGVvZihsb2NhbFN0b3JhZ2UpICE9PSAndW5kZWZpbmVkJykge1xuXHRcdFx0XHR2YXIgZWxtID0gZG9jdW1lbnQucXVlcnlTZWxlY3RvcignLicrIGFwcC5zaWRlYmFyLmNsYXNzICsnIFsnKyBhcHAuc2Nyb2xsQmFyLmF0dHIgKyddJyk7XG5cdFx0XHRcdFxuXHRcdFx0XHRpZiAoZWxtKSB7XG5cdFx0XHRcdFx0ZWxtLm9uc2Nyb2xsID0gZnVuY3Rpb24oKSB7XG5cdFx0XHRcdFx0XHRsb2NhbFN0b3JhZ2Uuc2V0SXRlbShhcHAuc2lkZWJhci5zY3JvbGxCYXIubG9jYWxTdG9yYWdlLCB0aGlzLnNjcm9sbFRvcCk7XG5cdFx0XHRcdFx0fVxuXHRcdFx0XHRcdHZhciBkZWZhdWx0U2Nyb2xsID0gbG9jYWxTdG9yYWdlLmdldEl0ZW0oYXBwLnNpZGViYXIuc2Nyb2xsQmFyLmxvY2FsU3RvcmFnZSk7XG5cdFx0XHRcdFx0aWYgKGRlZmF1bHRTY3JvbGwpIHtcblx0XHRcdFx0XHRcdGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3IoJy4nKyBhcHAuc2lkZWJhci5jbGFzcyArJyBbJysgYXBwLnNjcm9sbEJhci5hdHRyICsnXScpLnNjcm9sbFRvcCA9IGRlZmF1bHRTY3JvbGw7XG5cdFx0XHRcdFx0fVxuXHRcdFx0XHR9XG5cdFx0XHR9XG5cdFx0fSBjYXRjaCAoZXJyb3IpIHtcblx0XHRcdGNvbnNvbGUubG9nKGVycm9yKTtcblx0XHR9XG5cdH1cbn07XG5cblxuLyogMDQuIEhhbmRsZSBTaWRlYmFyIE1pbmlmeVxuLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tICovXG52YXIgaGFuZGxlU2lkZWJhck1pbmlmeSA9IGZ1bmN0aW9uKCkge1xuXHR2YXIgZWxtcyA9IFtdLnNsaWNlLmNhbGwoZG9jdW1lbnQucXVlcnlTZWxlY3RvckFsbCgnWycrIGFwcC5zaWRlYmFyLm1pbmlmeS50b2dnbGVBdHRyICsnXScpKTtcblx0ZWxtcy5tYXAoZnVuY3Rpb24oZWxtKSB7XG5cdFx0ZWxtLm9uY2xpY2sgPSBmdW5jdGlvbihlKSB7XG5cdFx0XHRlLnByZXZlbnREZWZhdWx0KCk7XG5cdFx0XG5cdFx0XHR2YXIgdGFyZ2V0RWxtID0gZG9jdW1lbnQucXVlcnlTZWxlY3RvcignLicrIGFwcC5jbGFzcyk7XG5cdFx0XHRcblx0XHRcdGlmICh0YXJnZXRFbG0pIHtcblx0XHRcdFx0aWYgKHRhcmdldEVsbS5jbGFzc0xpc3QuY29udGFpbnMoYXBwLnNpZGViYXIubWluaWZ5LnRvZ2dsZWRDbGFzcykpIHtcblx0XHRcdFx0XHR0YXJnZXRFbG0uY2xhc3NMaXN0LnJlbW92ZShhcHAuc2lkZWJhci5taW5pZnkudG9nZ2xlZENsYXNzKTtcblx0XHRcdFx0XHRsb2NhbFN0b3JhZ2UucmVtb3ZlSXRlbShhcHAuc2lkZWJhci5taW5pZnkubG9jYWxTdG9yYWdlKTtcblx0XHRcdFx0fSBlbHNlIHtcblx0XHRcdFx0XHR0YXJnZXRFbG0uY2xhc3NMaXN0LmFkZChhcHAuc2lkZWJhci5taW5pZnkudG9nZ2xlZENsYXNzKTtcblx0XHRcdFx0XHRsb2NhbFN0b3JhZ2Uuc2V0SXRlbShhcHAuc2lkZWJhci5taW5pZnkubG9jYWxTdG9yYWdlLCB0cnVlKTtcblx0XHRcdFx0fVxuXHRcdFx0fVxuXHRcdH07XG5cdH0pO1xuXHRcblx0aWYgKHR5cGVvZihTdG9yYWdlKSAhPT0gJ3VuZGVmaW5lZCcpIHtcblx0XHRpZiAobG9jYWxTdG9yYWdlW2FwcC5zaWRlYmFyLm1pbmlmeS5sb2NhbFN0b3JhZ2VdKSB7XG5cdFx0XHR2YXIgdGFyZ2V0RWxtID0gZG9jdW1lbnQucXVlcnlTZWxlY3RvcignLicrIGFwcC5jbGFzcyk7XG5cdFx0XHRcblx0XHRcdGlmICh0YXJnZXRFbG0pIHtcblx0XHRcdFx0dGFyZ2V0RWxtLmNsYXNzTGlzdC5hZGQoYXBwLnNpZGViYXIubWluaWZ5LnRvZ2dsZWRDbGFzcyk7XG5cdFx0XHR9XG5cdFx0fVxuXHR9XG59O1xudmFyIGhhbmRsZVNpZGViYXJNb2JpbGVUb2dnbGUgPSBmdW5jdGlvbigpIHtcblx0dmFyIGVsbXMgPSBbXS5zbGljZS5jYWxsKGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3JBbGwoJ1snKyBhcHAuc2lkZWJhci5tb2JpbGUudG9nZ2xlQXR0ciArJ10nKSk7XG5cdFxuXHRlbG1zLm1hcChmdW5jdGlvbihlbG0pIHtcblx0XHRlbG0ub25jbGljayA9IGZ1bmN0aW9uKGUpIHtcblx0XHRcdGUucHJldmVudERlZmF1bHQoKTtcblx0XHRcdFxuXHRcdFx0dmFyIHRhcmdldEVsbSA9IGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3IoJy4nKyBhcHAuY2xhc3MpXG5cdFx0XHRcblx0XHRcdGlmICh0YXJnZXRFbG0pIHtcblx0XHRcdFx0dGFyZ2V0RWxtLmNsYXNzTGlzdC5yZW1vdmUoYXBwLnNpZGViYXIubW9iaWxlLmNsb3NlZENsYXNzKTtcblx0XHRcdFx0dGFyZ2V0RWxtLmNsYXNzTGlzdC5hZGQoYXBwLnNpZGViYXIubW9iaWxlLnRvZ2dsZWRDbGFzcyk7XG5cdFx0XHR9XG5cdFx0fTtcblx0fSk7XG59O1xudmFyIGhhbmRsZVNpZGViYXJNb2JpbGVEaXNtaXNzID0gZnVuY3Rpb24oKSB7XG5cdHZhciBlbG1zID0gW10uc2xpY2UuY2FsbChkb2N1bWVudC5xdWVyeVNlbGVjdG9yQWxsKCdbJysgYXBwLnNpZGViYXIubW9iaWxlLmRpc21pc3NBdHRyICsnXScpKTtcblx0XG5cdGVsbXMubWFwKGZ1bmN0aW9uKGVsbSkge1xuXHRcdGVsbS5vbmNsaWNrID0gZnVuY3Rpb24oZSkge1xuXHRcdFx0ZS5wcmV2ZW50RGVmYXVsdCgpO1xuXHRcdFx0XG5cdFx0XHR2YXIgdGFyZ2V0RWxtID0gZG9jdW1lbnQucXVlcnlTZWxlY3RvcignLicrIGFwcC5jbGFzcylcblx0XHRcdFxuXHRcdFx0aWYgKHRhcmdldEVsbSkge1xuXHRcdFx0XHR0YXJnZXRFbG0uY2xhc3NMaXN0LnJlbW92ZShhcHAuc2lkZWJhci5tb2JpbGUudG9nZ2xlZENsYXNzKTtcblx0XHRcdFx0dGFyZ2V0RWxtLmNsYXNzTGlzdC5hZGQoYXBwLnNpZGViYXIubW9iaWxlLmNsb3NlZENsYXNzKTtcblx0XHRcdFx0XG5cdFx0XHRcdHNldFRpbWVvdXQoZnVuY3Rpb24oKSB7XG5cdFx0XHRcdFx0dGFyZ2V0RWxtLmNsYXNzTGlzdC5yZW1vdmUoYXBwLnNpZGViYXIubW9iaWxlLmNsb3NlZENsYXNzKTtcblx0XHRcdFx0fSwgYXBwLmFuaW1hdGlvbi5zcGVlZCk7XG5cdFx0XHR9XG5cdFx0fTtcblx0fSk7XG59O1xuXG5cbi8qIDA1LiBIYW5kbGUgU2lkZWJhciBNaW5pZnkgRmxvYXQgTWVudVxuLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tICovXG52YXIgaGFuZGxlR2V0SGlkZGVuTWVudUhlaWdodCA9IGZ1bmN0aW9uKGVsbSkge1xuXHRlbG0uc2V0QXR0cmlidXRlKCdzdHlsZScsICdwb3NpdGlvbjogYWJzb2x1dGU7IHZpc2liaWxpdHk6IGhpZGRlbjsgZGlzcGxheTogYmxvY2sgIWltcG9ydGFudCcpO1xuXHR2YXIgdGFyZ2V0SGVpZ2h0ICA9IGVsbS5jbGllbnRIZWlnaHQ7XG5cdGVsbS5yZW1vdmVBdHRyaWJ1dGUoJ3N0eWxlJyk7XG5cdHJldHVybiB0YXJnZXRIZWlnaHQ7XG59XG52YXIgaGFuZGxlU2lkZWJhck1pbmlmeUZsb2F0TWVudUNsaWNrID0gZnVuY3Rpb24oKSB7XG5cdHZhciBlbG1zID0gW10uc2xpY2UuY2FsbChkb2N1bWVudC5xdWVyeVNlbGVjdG9yQWxsKCcuJysgYXBwLmZsb2F0U3VibWVudS5jbGFzcyArJyAuJysgYXBwLnNpZGViYXIubWVudUl0ZW1DbGFzcyArJy4nKyBhcHAuc2lkZWJhci5tZW51SXRlbUhhc1N1YkNsYXNzICsnID4gLicrIGFwcC5zaWRlYmFyLm1lbnVMaW5rQ2xhc3MpKTtcblx0aWYgKGVsbXMpIHtcblx0XHRlbG1zLm1hcChmdW5jdGlvbihlbG0pIHtcblx0XHRcdGVsbS5vbmNsaWNrID0gZnVuY3Rpb24oZSkge1xuXHRcdFx0XHRlLnByZXZlbnREZWZhdWx0KCk7XG5cdFx0XHRcdHZhciB0YXJnZXRJdGVtID0gdGhpcy5jbG9zZXN0KCcuJyArIGFwcC5zaWRlYmFyLm1lbnVJdGVtQ2xhc3MpO1xuXHRcdFx0XHR2YXIgdGFyZ2V0ID0gdGFyZ2V0SXRlbS5xdWVyeVNlbGVjdG9yKCcuJyArIGFwcC5zaWRlYmFyLm1lbnVTdWJtZW51Q2xhc3MpO1xuXHRcdFx0XHR2YXIgdGFyZ2V0U3R5bGUgPSBnZXRDb21wdXRlZFN0eWxlKHRhcmdldCk7XG5cdFx0XHRcdHZhciBjbG9zZSA9ICh0YXJnZXRTdHlsZS5nZXRQcm9wZXJ0eVZhbHVlKCdkaXNwbGF5JykgIT0gJ25vbmUnKSA/IHRydWUgOiBmYWxzZTtcblx0XHRcdFx0dmFyIGV4cGFuZCA9ICh0YXJnZXRTdHlsZS5nZXRQcm9wZXJ0eVZhbHVlKCdkaXNwbGF5JykgIT0gJ25vbmUnKSA/IGZhbHNlIDogdHJ1ZTtcblx0XHRcdFx0XG5cdFx0XHRcdHNsaWRlVG9nZ2xlKHRhcmdldCk7XG5cdFx0XHRcdFxuXHRcdFx0XHR2YXIgbG9vcEhlaWdodCA9IHNldEludGVydmFsKGZ1bmN0aW9uKCkge1xuXHRcdFx0XHRcdHZhciB0YXJnZXRNZW51ID0gZG9jdW1lbnQucXVlcnlTZWxlY3RvcihhcHAuZmxvYXRTdWJtZW51LmlkKTtcblx0XHRcdFx0XHR2YXIgdGFyZ2V0TWVudUFycm93ID0gZG9jdW1lbnQucXVlcnlTZWxlY3RvcihhcHAuZmxvYXRTdWJtZW51LmFycm93LmlkKTtcblx0XHRcdFx0XHR2YXIgdGFyZ2V0TWVudUxpbmUgPSBkb2N1bWVudC5xdWVyeVNlbGVjdG9yKGFwcC5mbG9hdFN1Ym1lbnUubGluZS5pZCk7XG5cdFx0XHRcdFx0dmFyIHRhcmdldEhlaWdodCA9IHRhcmdldE1lbnUuY2xpZW50SGVpZ2h0O1xuXHRcdFx0XHRcdHZhciB0YXJnZXRPZmZzZXQgPSB0YXJnZXRNZW51LmdldEJvdW5kaW5nQ2xpZW50UmVjdCgpO1xuXHRcdFx0XHRcdHZhciB0YXJnZXRPcmlUb3AgPSB0YXJnZXRNZW51LmdldEF0dHJpYnV0ZSgnZGF0YS1vZmZzZXQtdG9wJyk7XG5cdFx0XHRcdFx0dmFyIHRhcmdldE1lbnVUb3AgPSB0YXJnZXRNZW51LmdldEF0dHJpYnV0ZSgnZGF0YS1tZW51LW9mZnNldC10b3AnKTtcblx0XHRcdFx0XHR2YXIgdGFyZ2V0VG9wIFx0ID0gdGFyZ2V0T2Zmc2V0LnRvcDtcblx0XHRcdFx0XHR2YXIgd2luZG93SGVpZ2h0ID0gZG9jdW1lbnQuYm9keS5jbGllbnRIZWlnaHQ7XG5cdFx0XHRcdFx0aWYgKGNsb3NlKSB7XG5cdFx0XHRcdFx0XHRpZiAodGFyZ2V0VG9wID4gdGFyZ2V0T3JpVG9wKSB7XG5cdFx0XHRcdFx0XHRcdHRhcmdldFRvcCA9ICh0YXJnZXRUb3AgPiB0YXJnZXRPcmlUb3ApID8gdGFyZ2V0T3JpVG9wIDogdGFyZ2V0VG9wO1xuXHRcdFx0XHRcdFx0XHR0YXJnZXRNZW51LnN0eWxlLnRvcCA9IHRhcmdldFRvcCArICdweCc7XG5cdFx0XHRcdFx0XHRcdHRhcmdldE1lbnUuc3R5bGUuYm90dG9tID0gJ2F1dG8nO1xuXHRcdFx0XHRcdFx0XHR0YXJnZXRNZW51QXJyb3cuc3R5bGUudG9wID0gJzIwcHgnO1xuXHRcdFx0XHRcdFx0XHR0YXJnZXRNZW51QXJyb3cuc3R5bGUuYm90dG9tID0gJ2F1dG8nO1xuXHRcdFx0XHRcdFx0XHR0YXJnZXRNZW51TGluZS5zdHlsZS50b3AgPSAnMjBweCc7XG5cdFx0XHRcdFx0XHRcdHRhcmdldE1lbnVMaW5lLnN0eWxlLmJvdHRvbSA9ICdhdXRvJztcblx0XHRcdFx0XHRcdH1cblx0XHRcdFx0XHR9XG5cdFx0XHRcdFx0aWYgKGV4cGFuZCkge1xuXHRcdFx0XHRcdFx0aWYgKCh3aW5kb3dIZWlnaHQgLSB0YXJnZXRUb3ApIDwgdGFyZ2V0SGVpZ2h0KSB7XG5cdFx0XHRcdFx0XHRcdHZhciBhcnJvd0JvdHRvbSA9ICh3aW5kb3dIZWlnaHQgLSB0YXJnZXRNZW51VG9wKSAtIDIyO1xuXHRcdFx0XHRcdFx0XHR0YXJnZXRNZW51LnN0eWxlLnRvcCA9ICdhdXRvJztcblx0XHRcdFx0XHRcdFx0dGFyZ2V0TWVudS5zdHlsZS5ib3R0b20gPSAwO1xuXHRcdFx0XHRcdFx0XHR0YXJnZXRNZW51QXJyb3cuc3R5bGUudG9wID0gJ2F1dG8nO1xuXHRcdFx0XHRcdFx0XHR0YXJnZXRNZW51QXJyb3cuc3R5bGUuYm90dG9tID0gYXJyb3dCb3R0b20gKyAncHgnO1xuXHRcdFx0XHRcdFx0XHR0YXJnZXRNZW51TGluZS5zdHlsZS50b3AgPSAnMjBweCc7XG5cdFx0XHRcdFx0XHRcdHRhcmdldE1lbnVMaW5lLnN0eWxlLmJvdHRvbSA9IGFycm93Qm90dG9tICsgJ3B4Jztcblx0XHRcdFx0XHRcdH1cblx0XHRcdFx0XHRcdHZhciBmbG9hdFN1Ym1lbnVFbG0gPSBkb2N1bWVudC5xdWVyeVNlbGVjdG9yKGFwcC5mbG9hdFN1Ym1lbnUuaWQgKyAnIC4nKyBhcHAuZmxvYXRTdWJtZW51LmNsYXNzKTtcblx0XHRcdFx0XHRcdGlmICh0YXJnZXRIZWlnaHQgPiB3aW5kb3dIZWlnaHQpIHtcblx0XHRcdFx0XHRcdFx0aWYgKGZsb2F0U3VibWVudUVsbSkge1xuXHRcdFx0XHRcdFx0XHRcdHZhciBzcGxpdENsYXNzID0gKGFwcC5mbG9hdFN1Ym1lbnUub3ZlcmZsb3cuY2xhc3MpLnNwbGl0KCcgJyk7XG5cdFx0XHRcdFx0XHRcdFx0Zm9yICh2YXIgaSA9IDA7IGkgPCBzcGxpdENsYXNzLmxlbmd0aDsgaSsrKSB7XG5cdFx0XHRcdFx0XHRcdFx0XHRmbG9hdFN1Ym1lbnVFbG0uY2xhc3NMaXN0LmFkZChzcGxpdENsYXNzW2ldKTtcblx0XHRcdFx0XHRcdFx0XHR9XG5cdFx0XHRcdFx0XHRcdH1cblx0XHRcdFx0XHRcdH1cblx0XHRcdFx0XHR9XG5cdFx0XHRcdH0sIDEpO1xuXHRcdFx0XHRzZXRUaW1lb3V0KGZ1bmN0aW9uKCkge1xuXHRcdFx0XHRcdGNsZWFySW50ZXJ2YWwobG9vcEhlaWdodCk7XG5cdFx0XHRcdH0sIGFwcC5hbmltYXRpb24uc3BlZWQpO1xuXHRcdFx0fVxuXHRcdH0pO1xuXHR9XG59XG52YXIgaGFuZGxlU2lkZWJhck1pbmlmeUZsb2F0TWVudSA9IGZ1bmN0aW9uKCkge1xuXHR2YXIgZWxtcyA9IFtdLnNsaWNlLmNhbGwoZG9jdW1lbnQucXVlcnlTZWxlY3RvckFsbCgnLicgKyBhcHAuc2lkZWJhci5jbGFzcyArICcgLicrIGFwcC5zaWRlYmFyLm1lbnVDbGFzcyArJyA+IC4nKyBhcHAuc2lkZWJhci5tZW51SXRlbUNsYXNzICsnLicrIGFwcC5zaWRlYmFyLm1lbnVJdGVtSGFzU3ViQ2xhc3MgKycgPiAuJysgYXBwLnNpZGViYXIubWVudUxpbmtDbGFzcyArJycpKTtcblx0aWYgKGVsbXMpIHtcblx0XHRlbG1zLm1hcChmdW5jdGlvbihlbG0pIHtcblx0XHRcdGVsbS5vbm1vdXNlZW50ZXIgPSBmdW5jdGlvbigpIHtcblx0XHRcdFx0dmFyIGFwcEVsbSA9IGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3IoJy4nICsgYXBwLmNsYXNzKTtcblx0XHRcdFx0XG5cdFx0XHRcdGlmIChhcHBFbG0gJiYgYXBwRWxtLmNsYXNzTGlzdC5jb250YWlucyhhcHAuc2lkZWJhci5taW5pZnkudG9nZ2xlZENsYXNzKSkge1xuXHRcdFx0XHRcdGNsZWFyVGltZW91dChhcHAuZmxvYXRTdWJtZW51LnRpbWVvdXQpO1xuXHRcdFx0XHRcdFxuXHRcdFx0XHRcdHZhciB0YXJnZXRNZW51ID0gdGhpcy5jbG9zZXN0KCcuJysgYXBwLnNpZGViYXIubWVudUl0ZW1DbGFzcykucXVlcnlTZWxlY3RvcignLicgKyBhcHAuc2lkZWJhci5tZW51U3VibWVudUNsYXNzKTtcblx0XHRcdFx0XHRpZiAoYXBwLmZsb2F0U3VibWVudS5kb20gPT0gdGhpcyAmJiBkb2N1bWVudC5xdWVyeVNlbGVjdG9yKGFwcC5mbG9hdFN1Ym1lbnUuY2xhc3MpKSB7XG5cdFx0XHRcdFx0XHRyZXR1cm47XG5cdFx0XHRcdFx0fSBlbHNlIHtcblx0XHRcdFx0XHRcdGFwcC5mbG9hdFN1Ym1lbnUuZG9tID0gdGhpcztcblx0XHRcdFx0XHR9XG5cdFx0XHRcdFx0dmFyIHRhcmdldE1lbnVIdG1sID0gdGFyZ2V0TWVudS5pbm5lckhUTUw7XG5cdFx0XHRcdFx0aWYgKHRhcmdldE1lbnVIdG1sKSB7XG5cdFx0XHRcdFx0XHR2YXIgYm9keVN0eWxlICAgICA9IGdldENvbXB1dGVkU3R5bGUoZG9jdW1lbnQuYm9keSk7XG5cdFx0XHRcdFx0XHR2YXIgc2lkZWJhck9mZnNldCA9IGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3IoJy4nKyBhcHAuc2lkZWJhci5jbGFzcykuZ2V0Qm91bmRpbmdDbGllbnRSZWN0KCk7XG5cdFx0XHRcdFx0XHR2YXIgc2lkZWJhcldpZHRoICA9IHBhcnNlSW50KGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3IoJy4nKyBhcHAuc2lkZWJhci5jbGFzcykuY2xpZW50V2lkdGgpO1xuXHRcdFx0XHRcdFx0dmFyIHNpZGViYXJYICAgICAgPSAoYm9keVN0eWxlLmdldFByb3BlcnR5VmFsdWUoJ2RpcmVjdGlvbicpICE9ICdydGwnKSA/IChzaWRlYmFyT2Zmc2V0LmxlZnQgKyBzaWRlYmFyV2lkdGgpIDogKGRvY3VtZW50LmJvZHkuY2xpZW50V2lkdGggLSBzaWRlYmFyT2Zmc2V0LmxlZnQpO1xuXHRcdFx0XHRcdFx0dmFyIHRhcmdldEhlaWdodCAgPSBoYW5kbGVHZXRIaWRkZW5NZW51SGVpZ2h0KHRhcmdldE1lbnUpO1xuXHRcdFx0XHRcdFx0dmFyIHRhcmdldE9mZnNldCAgPSB0aGlzLmdldEJvdW5kaW5nQ2xpZW50UmVjdCgpO1xuXHRcdFx0XHRcdFx0dmFyIHRhcmdldFRvcCAgICAgPSB0YXJnZXRPZmZzZXQudG9wO1xuXHRcdFx0XHRcdFx0dmFyIHRhcmdldExlZnQgICAgPSAoYm9keVN0eWxlLmdldFByb3BlcnR5VmFsdWUoJ2RpcmVjdGlvbicpICE9ICdydGwnKSA/IHNpZGViYXJYIDogJ2F1dG8nO1xuXHRcdFx0XHRcdFx0dmFyIHRhcmdldFJpZ2h0ICAgPSAoYm9keVN0eWxlLmdldFByb3BlcnR5VmFsdWUoJ2RpcmVjdGlvbicpICE9ICdydGwnKSA/ICdhdXRvJyA6IHNpZGViYXJYO1xuXHRcdFx0XHRcdFx0dmFyIHdpbmRvd0hlaWdodCAgPSBkb2N1bWVudC5ib2R5LmNsaWVudEhlaWdodDtcblx0XHRcdFx0XHRcdFxuXHRcdFx0XHRcdFx0aWYgKCFkb2N1bWVudC5xdWVyeVNlbGVjdG9yKCcjJysgYXBwLmZsb2F0U3VibWVudS5pZCkpIHtcblx0XHRcdFx0XHRcdFx0dmFyIG92ZXJmbG93Q2xhc3MgPSAnJztcblx0XHRcdFx0XHRcdFx0aWYgKHRhcmdldEhlaWdodCA+IHdpbmRvd0hlaWdodCkge1xuXHRcdFx0XHRcdFx0XHRcdG92ZXJmbG93Q2xhc3MgPSBhcHAuZmxvYXRTdWJtZW51Lm92ZXJmbG93LmNsYXNzO1xuXHRcdFx0XHRcdFx0XHR9XG5cdFx0XHRcdFx0XHRcdHZhciBodG1sID0gZG9jdW1lbnQuY3JlYXRlRWxlbWVudCgnZGl2Jyk7XG5cdFx0XHRcdFx0XHRcdGh0bWwuc2V0QXR0cmlidXRlKCdpZCcsIGFwcC5mbG9hdFN1Ym1lbnUuaWQpO1xuXHRcdFx0XHRcdFx0XHRodG1sLnNldEF0dHJpYnV0ZSgnY2xhc3MnLCBhcHAuZmxvYXRTdWJtZW51LmNsYXNzKTtcblx0XHRcdFx0XHRcdFx0aHRtbC5zZXRBdHRyaWJ1dGUoJ2RhdGEtb2Zmc2V0LXRvcCcsIHRhcmdldFRvcCk7XG5cdFx0XHRcdFx0XHRcdGh0bWwuc2V0QXR0cmlidXRlKCdkYXRhLW1lbnUtb2Zmc2V0LXRvcCcsIHRhcmdldFRvcCk7XG5cdFx0XHRcdFx0XHRcdGh0bWwuaW5uZXJIVE1MID0gJycrXG5cdFx0XHRcdFx0XHRcdCdcdDxkaXYgY2xhc3M9XCInKyBhcHAuZmxvYXRTdWJtZW51LmNvbnRhaW5lci5jbGFzcyArJyAnKyBvdmVyZmxvd0NsYXNzICsnXCI+JysgdGFyZ2V0TWVudUh0bWwgKyAnPC9kaXY+Jztcblx0XHRcdFx0XHRcdFx0YXBwRWxtLmFwcGVuZENoaWxkKGh0bWwpO1xuXHRcdFx0XHRcdFx0XHRcblx0XHRcdFx0XHRcdFx0dmFyIGVsbSA9IGRvY3VtZW50LmdldEVsZW1lbnRCeUlkKGFwcC5mbG9hdFN1Ym1lbnUuaWQpO1xuXHRcdFx0XHRcdFx0XHRlbG0ub25tb3VzZW92ZXIgPSBmdW5jdGlvbigpIHtcblx0XHRcdFx0XHRcdFx0XHRjbGVhclRpbWVvdXQoYXBwLmZsb2F0U3VibWVudS50aW1lb3V0KTtcblx0XHRcdFx0XHRcdFx0fTtcblx0XHRcdFx0XHRcdFx0ZWxtLm9ubW91c2VvdXQgPSBmdW5jdGlvbigpIHtcblx0XHRcdFx0XHRcdFx0XHRhcHAuZmxvYXRTdWJtZW51LnRpbWVvdXQgPSBzZXRUaW1lb3V0KCgpID0+IHtcblx0XHRcdFx0XHRcdFx0XHRcdGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3IoJyMnKyBhcHAuZmxvYXRTdWJtZW51LmlkKS5yZW1vdmUoKTtcblx0XHRcdFx0XHRcdFx0XHR9LCBhcHAuYW5pbWF0aW9uLnNwZWVkKTtcblx0XHRcdFx0XHRcdFx0fTtcblx0XHRcdFx0XHRcdH0gZWxzZSB7XG5cdFx0XHRcdFx0XHRcdHZhciBmbG9hdFN1Ym1lbnUgPSBkb2N1bWVudC5xdWVyeVNlbGVjdG9yKCcjJysgYXBwLmZsb2F0U3VibWVudS5pZCk7XG5cdFx0XHRcdFx0XHRcdHZhciBmbG9hdFN1Ym1lbnVFbG0gPSBkb2N1bWVudC5xdWVyeVNlbGVjdG9yKCcjJysgYXBwLmZsb2F0U3VibWVudS5pZCArICcgLicrIGFwcC5mbG9hdFN1Ym1lbnUuY29udGFpbmVyLmNsYXNzKTtcblx0XHRcdFx0XHRcdFx0XG5cdFx0XHRcdFx0XHRcdGlmICh0YXJnZXRIZWlnaHQgPiB3aW5kb3dIZWlnaHQpIHtcblx0XHRcdFx0XHRcdFx0XHRpZiAoZmxvYXRTdWJtZW51RWxtKSB7XG5cdFx0XHRcdFx0XHRcdFx0XHR2YXIgc3BsaXRDbGFzcyA9IChhcHAuZmxvYXRTdWJtZW51Lm92ZXJmbG93LmNsYXNzKS5zcGxpdCgnICcpO1xuXHRcdFx0XHRcdFx0XHRcdFx0Zm9yICh2YXIgaSA9IDA7IGkgPCBzcGxpdENsYXNzLmxlbmd0aDsgaSsrKSB7XG5cdFx0XHRcdFx0XHRcdFx0XHRcdGZsb2F0U3VibWVudUVsbS5jbGFzc0xpc3QuYWRkKHNwbGl0Q2xhc3NbaV0pO1xuXHRcdFx0XHRcdFx0XHRcdFx0fVxuXHRcdFx0XHRcdFx0XHRcdH1cblx0XHRcdFx0XHRcdFx0fVxuXHRcdFx0XHRcdFx0XHRmbG9hdFN1Ym1lbnUuc2V0QXR0cmlidXRlKCdkYXRhLW9mZnNldC10b3AnLCB0YXJnZXRUb3ApO1xuXHRcdFx0XHRcdFx0XHRmbG9hdFN1Ym1lbnUuc2V0QXR0cmlidXRlKCdkYXRhLW1lbnUtb2Zmc2V0LXRvcCcsIHRhcmdldFRvcCk7XG5cdFx0XHRcdFx0XHRcdGZsb2F0U3VibWVudUVsbS5pbm5lckhUTUwgPSB0YXJnZXRNZW51SHRtbDtcblx0XHRcdFx0XHRcdH1cblx0XHRcdFx0XG5cdFx0XHRcdFx0XHR2YXIgdGFyZ2V0SGVpZ2h0ID0gZG9jdW1lbnQucXVlcnlTZWxlY3RvcignIycrIGFwcC5mbG9hdFN1Ym1lbnUuaWQpLmNsaWVudEhlaWdodDtcblx0XHRcdFx0XHRcdHZhciBmbG9hdFN1Ym1lbnVFbG0gPSBkb2N1bWVudC5xdWVyeVNlbGVjdG9yKCcjJysgYXBwLmZsb2F0U3VibWVudS5pZCk7XG5cdFx0XHRcdFx0XHRpZiAoKHdpbmRvd0hlaWdodCAtIHRhcmdldFRvcCkgPiB0YXJnZXRIZWlnaHQpIHtcblx0XHRcdFx0XHRcdFx0aWYgKGZsb2F0U3VibWVudUVsbSkge1xuXHRcdFx0XHRcdFx0XHRcdGZsb2F0U3VibWVudUVsbS5zdHlsZS50b3AgPSB0YXJnZXRUb3AgKyAncHgnO1xuXHRcdFx0XHRcdFx0XHRcdGZsb2F0U3VibWVudUVsbS5zdHlsZS5sZWZ0ID0gdGFyZ2V0TGVmdCArICdweCc7XG5cdFx0XHRcdFx0XHRcdFx0ZmxvYXRTdWJtZW51RWxtLnN0eWxlLmJvdHRvbSA9ICdhdXRvJztcblx0XHRcdFx0XHRcdFx0XHRmbG9hdFN1Ym1lbnVFbG0uc3R5bGUucmlnaHQgPSB0YXJnZXRSaWdodCArICdweCc7XG5cdFx0XHRcdFx0XHRcdH1cblx0XHRcdFx0XHRcdH0gZWxzZSB7XG5cdFx0XHRcdFx0XHRcdHZhciBhcnJvd0JvdHRvbSA9ICh3aW5kb3dIZWlnaHQgLSB0YXJnZXRUb3ApIC0gMjE7XG5cdFx0XHRcdFx0XHRcdGlmIChmbG9hdFN1Ym1lbnVFbG0pIHtcblx0XHRcdFx0XHRcdFx0XHRmbG9hdFN1Ym1lbnVFbG0uc3R5bGUudG9wID0gJ2F1dG8nO1xuXHRcdFx0XHRcdFx0XHRcdGZsb2F0U3VibWVudUVsbS5zdHlsZS5sZWZ0ID0gdGFyZ2V0TGVmdCArICdweCc7XG5cdFx0XHRcdFx0XHRcdFx0ZmxvYXRTdWJtZW51RWxtLnN0eWxlLmJvdHRvbSA9IDA7XG5cdFx0XHRcdFx0XHRcdFx0ZmxvYXRTdWJtZW51RWxtLnN0eWxlLnJpZ2h0ID0gdGFyZ2V0UmlnaHQgKyAncHgnO1xuXHRcdFx0XHRcdFx0XHR9XG5cdFx0XHRcdFx0XHR9XG5cdFx0XHRcdFx0XHRoYW5kbGVTaWRlYmFyTWluaWZ5RmxvYXRNZW51Q2xpY2soKTtcblx0XHRcdFx0XHR9IGVsc2Uge1xuXHRcdFx0XHRcdFx0ZG9jdW1lbnQucXVlcnlTZWxlY3RvcignIycrIGFwcC5mbG9hdFN1Ym1lbnUuaWQpLnJlbW92ZSgpO1xuXHRcdFx0XHRcdFx0YXBwLmZsb2F0U3VibWVudS5kb20gPSAnJztcblx0XHRcdFx0XHR9XG5cdFx0XHRcdH1cblx0XHRcdH1cblx0XHRcdGVsbS5vbm1vdXNlbGVhdmUgPSBmdW5jdGlvbigpIHtcblx0XHRcdFx0dmFyIGVsbSA9IGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3IoJy4nICsgYXBwLmNsYXNzKTtcblx0XHRcdFx0aWYgKGVsbSAmJiBlbG0uY2xhc3NMaXN0LmNvbnRhaW5zKGFwcC5zaWRlYmFyLm1pbmlmeS50b2dnbGVkQ2xhc3MpKSB7XG5cdFx0XHRcdFx0YXBwLmZsb2F0U3VibWVudS50aW1lb3V0ID0gc2V0VGltZW91dCgoKSA9PiB7XG5cdFx0XHRcdFx0XHRkb2N1bWVudC5xdWVyeVNlbGVjdG9yKCcjJysgYXBwLmZsb2F0U3VibWVudS5pZCkucmVtb3ZlKCk7XG5cdFx0XHRcdFx0XHRhcHAuZmxvYXRTdWJtZW51LmRvbSA9ICcnO1xuXHRcdFx0XHRcdH0sIDI1MCk7XG5cdFx0XHRcdH1cblx0XHRcdH1cblx0XHR9KTtcblx0fVxufTtcblxuXG4vKiAwNy4gSGFuZGxlIENhcmQgLSBFeHBhbmRcbi0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLSAqL1xudmFyIGhhbmRsZUNhcmRBY3Rpb24gPSBmdW5jdGlvbigpIHtcblx0XCJ1c2Ugc3RyaWN0XCI7XG5cblx0aWYgKGFwcC5jYXJkLmV4cGFuZC5zdGF0dXMpIHtcblx0XHRyZXR1cm4gZmFsc2U7XG5cdH1cblx0YXBwLmNhcmQuZXhwYW5kU3RhdHVzID0gdHJ1ZTtcblxuXHQvLyBleHBhbmRcblx0dmFyIGV4cGFuZFRvZ2dsZXJMaXN0ID0gW10uc2xpY2UuY2FsbChkb2N1bWVudC5xdWVyeVNlbGVjdG9yQWxsKCdbJysgYXBwLmNhcmQuZXhwYW5kLnRvZ2dsZUF0dHIgKyddJykpO1xuXHR2YXIgZXhwYW5kVG9nZ2xlclRvb2x0aXBMaXN0ID0gZXhwYW5kVG9nZ2xlckxpc3QubWFwKGZ1bmN0aW9uIChleHBhbmRUb2dnbGVyRWwpIHtcblx0XHRleHBhbmRUb2dnbGVyRWwub25jbGljayA9IGZ1bmN0aW9uKGUpIHtcblx0XHRcdGUucHJldmVudERlZmF1bHQoKTtcblx0XHRcblx0XHRcdHZhciB0YXJnZXQgPSB0aGlzLmNsb3Nlc3QoJy4nKyBhcHAuY2FyZC5jbGFzcyk7XG5cdFx0XHR2YXIgdGFyZ2V0Q2xhc3MgPSBhcHAuY2FyZC5leHBhbmQuY2xhc3M7XG5cblx0XHRcdGlmIChkb2N1bWVudC5ib2R5LmNsYXNzTGlzdC5jb250YWlucyh0YXJnZXRDbGFzcykgJiYgdGFyZ2V0LmNsYXNzTGlzdC5jb250YWlucyh0YXJnZXRDbGFzcykpIHtcblx0XHRcdFx0dGFyZ2V0LnJlbW92ZUF0dHJpYnV0ZSgnc3R5bGUnKTtcblx0XHRcdFx0dGFyZ2V0LmNsYXNzTGlzdC5yZW1vdmUodGFyZ2V0Q2xhc3MpO1xuXHRcdFx0XHRkb2N1bWVudC5ib2R5LmNsYXNzTGlzdC5yZW1vdmUodGFyZ2V0Q2xhc3MpO1xuXHRcdFx0fSBlbHNlIHtcblx0XHRcdFx0ZG9jdW1lbnQuYm9keS5jbGFzc0xpc3QuYWRkKHRhcmdldENsYXNzKTtcblx0XHRcdFx0dGFyZ2V0LmNsYXNzTGlzdC5hZGQodGFyZ2V0Q2xhc3MpO1xuXHRcdFx0fVxuXHRcdFxuXHRcdFx0d2luZG93LmRpc3BhdGNoRXZlbnQobmV3IEV2ZW50KCdyZXNpemUnKSk7XG5cdFx0fTtcblx0XG5cdFx0cmV0dXJuIG5ldyBib290c3RyYXAuVG9vbHRpcChleHBhbmRUb2dnbGVyRWwsIHtcblx0XHRcdHRpdGxlOiBhcHAuY2FyZC5leHBhbmQudG9vbHRpcFRleHQsXG5cdFx0XHRwbGFjZW1lbnQ6ICdib3R0b20nLFxuXHRcdFx0dHJpZ2dlcjogJ2hvdmVyJyxcblx0XHRcdGNvbnRhaW5lcjogJ2JvZHknXG5cdFx0fSk7XG5cdH0pO1xufTtcblxuXG4vKiAwOC4gSGFuZGxlIFRvb2x0aXAgJiBQb3BvdmVyIEFjdGl2YXRpb25cbi0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLSAqL1xudmFyIGhhbmRlbFRvb2x0aXBQb3BvdmVyQWN0aXZhdGlvbiA9IGZ1bmN0aW9uKCkge1xuXHRcInVzZSBzdHJpY3RcIjtcblx0dmFyIHRvb2x0aXBUcmlnZ2VyTGlzdCA9IFtdLnNsaWNlLmNhbGwoZG9jdW1lbnQucXVlcnlTZWxlY3RvckFsbCgnWycrIGFwcC50b29sdGlwLnRvZ2dsZUF0dHIgKyddJykpXG5cdHZhciB0b29sdGlwTGlzdCA9IHRvb2x0aXBUcmlnZ2VyTGlzdC5tYXAoZnVuY3Rpb24gKHRvb2x0aXBUcmlnZ2VyRWwpIHtcblx0XHRyZXR1cm4gbmV3IGJvb3RzdHJhcC5Ub29sdGlwKHRvb2x0aXBUcmlnZ2VyRWwpO1xuXHR9KTtcblx0XG5cdHZhciBwb3BvdmVyVHJpZ2dlckxpc3QgPSBbXS5zbGljZS5jYWxsKGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3JBbGwoJ1snKyBhcHAucG9wb3Zlci50b2dnbGVBdHRyICsnXScpKVxuXHR2YXIgcG9wb3Zlckxpc3QgPSBwb3BvdmVyVHJpZ2dlckxpc3QubWFwKGZ1bmN0aW9uIChwb3BvdmVyVHJpZ2dlckVsKSB7XG5cdFx0cmV0dXJuIG5ldyBib290c3RyYXAuUG9wb3Zlcihwb3BvdmVyVHJpZ2dlckVsKTtcblx0fSk7XG59O1xuXG5cbi8qIDA5LiBIYW5kbGUgU2Nyb2xsIHRvIFRvcCBCdXR0b24gQWN0aXZhdGlvblxuLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tICovXG52YXIgaGFuZGxlU2Nyb2xsVG9Ub3BCdXR0b24gPSBmdW5jdGlvbigpIHtcblx0XCJ1c2Ugc3RyaWN0XCI7XG5cdHZhciBlbG1UcmlnZ2VyTGlzdCA9IFtdLnNsaWNlLmNhbGwoZG9jdW1lbnQucXVlcnlTZWxlY3RvckFsbCgnWycrIGFwcC5zY3JvbGxUb3BCdXR0b24udG9nZ2xlQXR0ciArJ10nKSk7XG5cdFxuXHRkb2N1bWVudC5vbnNjcm9sbCA9IGZ1bmN0aW9uKCkge1xuXHRcdHZhciBkb2MgPSBkb2N1bWVudC5kb2N1bWVudEVsZW1lbnQ7XG5cdFx0dmFyIHRvdGFsU2Nyb2xsID0gKHdpbmRvdy5wYWdlWU9mZnNldCB8fCBkb2Muc2Nyb2xsVG9wKSAgLSAoZG9jLmNsaWVudFRvcCB8fCAwKTtcblx0XHR2YXIgZWxtTGlzdCA9IGVsbVRyaWdnZXJMaXN0Lm1hcChmdW5jdGlvbihlbG0pIHtcblx0XHRcdGlmICh0b3RhbFNjcm9sbCA+PSAyMDApIHtcblx0XHRcdFx0aWYgKCFlbG0uY2xhc3NMaXN0LmNvbnRhaW5zKGFwcC5zY3JvbGxUb3BCdXR0b24uc2hvd0NsYXNzKSkge1xuXHRcdFx0XHRcdGVsbS5jbGFzc0xpc3QuYWRkKGFwcC5zY3JvbGxUb3BCdXR0b24uc2hvd0NsYXNzKTtcblx0XHRcdFx0fVxuXHRcdFx0fSBlbHNlIHtcblx0XHRcdFx0ZWxtLmNsYXNzTGlzdC5yZW1vdmUoYXBwLnNjcm9sbFRvcEJ1dHRvbi5zaG93Q2xhc3MpO1xuXHRcdFx0fVxuXHRcdH0pO1xuXHR9XG5cdFxuXHR2YXIgZWxtTGlzdCA9IGVsbVRyaWdnZXJMaXN0Lm1hcChmdW5jdGlvbihlbG0pIHtcblx0XHRlbG0ub25jbGljayA9IGZ1bmN0aW9uKGUpIHtcblx0XHRcdGUucHJldmVudERlZmF1bHQoKTtcblx0XHRcdFxuXHRcdFx0d2luZG93LnNjcm9sbFRvKHt0b3A6IDAsIGJlaGF2aW9yOiAnc21vb3RoJ30pO1xuXHRcdH1cblx0fSk7XG59O1xuXG5cbi8qIDExLiBIYW5kbGUgU2Nyb2xsIHRvXG4tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0gKi9cbnZhciBoYW5kbGVTY3JvbGxUbyA9IGZ1bmN0aW9uKCkge1xuXHR2YXIgZWxtVHJpZ2dlckxpc3QgPSBbXS5zbGljZS5jYWxsKGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3JBbGwoJ1snKyBhcHAuc2Nyb2xsVG8udG9nZ2xlQXR0ciArJ10nKSk7XG5cdHZhciBlbG1MaXN0ID0gZWxtVHJpZ2dlckxpc3QubWFwKGZ1bmN0aW9uKGVsbSkge1xuXHRcdGVsbS5vbmNsaWNrID0gZnVuY3Rpb24oZSkge1xuXHRcdFx0ZS5wcmV2ZW50RGVmYXVsdCgpO1xuXHRcdFxuXHRcdFx0dmFyIHRhcmdldEF0dHIgPSAoZWxtLmdldEF0dHJpYnV0ZShhcHAuc2Nyb2xsVG8udGFyZ2V0QXR0cikpID8gdGhpcy5nZXRBdHRyaWJ1dGUoYXBwLnNjcm9sbFRvLnRhcmdldEF0dHIpIDogdGhpcy5nZXRBdHRyaWJ1dGUoJ2hyZWYnKTtcblx0XHRcdHZhciB0YXJnZXRFbG0gPSBkb2N1bWVudC5xdWVyeVNlbGVjdG9yQWxsKHRhcmdldEF0dHIpWzBdO1xuXHRcdFx0dmFyIHRhcmdldEhlYWRlciA9IGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3JBbGwoJy4nKyBhcHAuaGVhZGVyLmNsYXNzKVswXTtcblx0XHRcdHZhciB0YXJnZXRIZWlnaHQgPSB0YXJnZXRIZWFkZXIub2Zmc2V0SGVpZ2h0O1xuXHRcdFx0aWYgKHRhcmdldEVsbSkge1xuXHRcdFx0XHR2YXIgdGFyZ2V0VG9wID0gdGFyZ2V0RWxtLm9mZnNldFRvcCAtIHRhcmdldEhlaWdodCArIDM2O1xuXHRcdFx0XHR3aW5kb3cuc2Nyb2xsVG8oe3RvcDogdGFyZ2V0VG9wLCBiZWhhdmlvcjogJ3Ntb290aCd9KTtcblx0XHRcdH1cblx0XHR9XG5cdH0pO1xufTtcblxuXG4vKiAxMi4gSGFuZGxlIFRoZW1lIFBhbmVsIEV4cGFuZFxuLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tICovXG52YXIgaGFuZGxlVGhlbWVQYW5lbEV4cGFuZCA9IGZ1bmN0aW9uKCkge1xuXHR2YXIgZWxtTGlzdCA9IFtdLnNsaWNlLmNhbGwoZG9jdW1lbnQucXVlcnlTZWxlY3RvckFsbCgnWycrIGFwcC50aGVtZVBhbmVsLnRvZ2dsZUF0dHIgKyddJykpO1xuXHRcblx0ZWxtTGlzdC5tYXAoZnVuY3Rpb24oZWxtKSB7XG5cdFx0ZWxtLm9uY2xpY2sgPSBmdW5jdGlvbihlKSB7XG5cdFx0XHRlLnByZXZlbnREZWZhdWx0KCk7XG5cdFx0XHRcblx0XHRcdHZhciB0YXJnZXRDb250YWluZXIgPSBkb2N1bWVudC5xdWVyeVNlbGVjdG9yKCcuJysgYXBwLnRoZW1lUGFuZWwuY2xhc3MpO1xuXHRcdFx0dmFyIHRhcmdldEV4cGFuZCA9IGZhbHNlO1xuXHRcdFxuXHRcdFx0aWYgKHRhcmdldENvbnRhaW5lci5jbGFzc0xpc3QuY29udGFpbnMoYXBwLnRoZW1lUGFuZWwuYWN0aXZlQ2xhc3MpKSB7XG5cdFx0XHRcdHRhcmdldENvbnRhaW5lci5jbGFzc0xpc3QucmVtb3ZlKGFwcC50aGVtZVBhbmVsLmFjdGl2ZUNsYXNzKTtcblx0XHRcdFx0c2V0Q29va2llKGFwcC50aGVtZVBhbmVsLmV4cGFuZENvb2tpZSwgJycpO1xuXHRcdFx0fSBlbHNlIHtcblx0XHRcdFx0dGFyZ2V0Q29udGFpbmVyLmNsYXNzTGlzdC5hZGQoYXBwLnRoZW1lUGFuZWwuYWN0aXZlQ2xhc3MpO1xuXHRcdFx0XHRzZXRDb29raWUoYXBwLnRoZW1lUGFuZWwuZXhwYW5kQ29va2llLCBhcHAudGhlbWVQYW5lbC5leHBhbmRDb29raWVWYWx1ZSk7XG5cdFx0XHR9XG5cdFx0fVxuXHR9KTtcblx0XG5cdGlmIChnZXRDb29raWUoYXBwLnRoZW1lUGFuZWwuZXhwYW5kQ29va2llKSAmJiBnZXRDb29raWUoYXBwLnRoZW1lUGFuZWwuZXhwYW5kQ29va2llKSA9PSBhcHAudGhlbWVQYW5lbC5leHBhbmRDb29raWVWYWx1ZSkge1xuXHRcdHZhciBlbG0gPSBkb2N1bWVudC5xdWVyeVNlbGVjdG9yKCdbJysgYXBwLnRoZW1lUGFuZWwudG9nZ2xlQXR0ciArJ10nKTtcblx0XHRpZiAoZWxtKSB7XG5cdFx0XHRlbG0uY2xpY2soKTtcblx0XHR9XG5cdH1cbn07XG5cblxuLyogMTMuIEhhbmRsZSBUaGVtZSBQYWdlIENvbnRyb2xcbi0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLSAqL1xudmFyIGhhbmRsZVRoZW1lUGFnZUNvbnRyb2wgPSBmdW5jdGlvbigpIHtcblx0Ly8gVGhlbWUgQ2xpY2tcblx0dmFyIGVsbXMgPSBbXS5zbGljZS5jYWxsKGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3JBbGwoJy4nKyBhcHAudGhlbWVQYW5lbC50aGVtZUxpc3QuY2xhc3MgKycgWycrIGFwcC50aGVtZVBhbmVsLnRoZW1lTGlzdC50b2dnbGVBdHRyICsnXScpKTtcblx0ZWxtcy5tYXAoZnVuY3Rpb24oZWxtKSB7XG5cdFx0ZWxtLm9uY2xpY2sgPSBmdW5jdGlvbigpIHtcblx0XHRcdHZhciB0YXJnZXRUaGVtZUNsYXNzID0gdGhpcy5nZXRBdHRyaWJ1dGUoYXBwLnRoZW1lUGFuZWwudGhlbWVMaXN0LnRvZ2dsZUF0dHIpO1xuXHRcdFx0Zm9yICh2YXIgeCA9IDA7IHggPCBkb2N1bWVudC5ib2R5LmNsYXNzTGlzdC5sZW5ndGg7IHgrKykge1xuXHRcdFx0XHR2YXIgdGFyZ2V0Q2xhc3MgPSBkb2N1bWVudC5ib2R5LmNsYXNzTGlzdFt4XTtcblx0XHRcdFx0aWYgKHRhcmdldENsYXNzLnNlYXJjaCgndGhlbWUtJykgPiAtMSkge1xuXHRcdFx0XHRcdGRvY3VtZW50LmJvZHkuY2xhc3NMaXN0LnJlbW92ZSh0YXJnZXRDbGFzcyk7XG5cdFx0XHRcdH1cblx0XHRcdH1cblx0XHRcdGlmICh0YXJnZXRUaGVtZUNsYXNzKSB7XG5cdFx0XHRcdGRvY3VtZW50LmJvZHkuY2xhc3NMaXN0LmFkZCh0YXJnZXRUaGVtZUNsYXNzKTtcblx0XHRcdH1cblx0XHRcblx0XHRcdHZhciB0b2dnbGVycyA9IFtdLnNsaWNlLmNhbGwoZG9jdW1lbnQucXVlcnlTZWxlY3RvckFsbCgnLicrIGFwcC50aGVtZVBhbmVsLnRoZW1lTGlzdC5jbGFzcyArJyBbJysgYXBwLnRoZW1lUGFuZWwudGhlbWVMaXN0LnRvZ2dsZUF0dHIgKyddJykpO1xuXHRcdFx0dG9nZ2xlcnMubWFwKGZ1bmN0aW9uKHRvZ2dsZXIpIHtcblx0XHRcdFx0aWYgKHRvZ2dsZXIgIT0gZWxtKSB7XG5cdFx0XHRcdFx0dG9nZ2xlci5jbG9zZXN0KCdsaScpLmNsYXNzTGlzdC5yZW1vdmUoYXBwLnRoZW1lUGFuZWwudGhlbWVMaXN0LmFjdGl2ZUNsYXNzKTtcblx0XHRcdFx0fSBlbHNlIHtcblx0XHRcdFx0XHR0b2dnbGVyLmNsb3Nlc3QoJ2xpJykuY2xhc3NMaXN0LmFkZChhcHAudGhlbWVQYW5lbC50aGVtZUxpc3QuYWN0aXZlQ2xhc3MpO1xuXHRcdFx0XHR9XG5cdFx0XHR9KTtcblx0XHRcdHNldENvb2tpZShhcHAudGhlbWVQYW5lbC50aGVtZUxpc3QuY29va2llTmFtZSwgdGFyZ2V0VGhlbWVDbGFzcyk7XG5cdFx0XHRkb2N1bWVudC5kaXNwYXRjaEV2ZW50KG5ldyBDdXN0b21FdmVudChhcHAudGhlbWVQYW5lbC50aGVtZUxpc3Qub25DaGFuZ2VFdmVudCkpO1xuXHRcdH1cblx0fSk7XG5cdFxuXHQvLyBUaGVtZSBDb29raWVcblx0aWYgKGdldENvb2tpZShhcHAudGhlbWVQYW5lbC50aGVtZUxpc3QuY29va2llTmFtZSkgJiYgZG9jdW1lbnQucXVlcnlTZWxlY3RvcignLicrIGFwcC50aGVtZVBhbmVsLnRoZW1lTGlzdC5jbGFzcykpIHtcblx0XHR2YXIgdGFyZ2V0RWxtID0gZG9jdW1lbnQucXVlcnlTZWxlY3RvcignLicrIGFwcC50aGVtZVBhbmVsLnRoZW1lTGlzdC5jbGFzcyArJyBbJysgYXBwLnRoZW1lUGFuZWwudGhlbWVMaXN0LnRvZ2dsZUF0dHIgKyc9XCInKyBnZXRDb29raWUoYXBwLnRoZW1lUGFuZWwudGhlbWVMaXN0LmNvb2tpZU5hbWUpICsnXCJdJyk7XG5cdFx0aWYgKHRhcmdldEVsbSkge1xuXHRcdFx0dGFyZ2V0RWxtLmNsaWNrKCk7XG5cdFx0fVxuXHR9XG5cdFxuXHQvLyBEYXJrIE1vZGUgQ2xpY2tcblx0dmFyIGVsbXMgPSBbXS5zbGljZS5jYWxsKGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3JBbGwoJy4nKyBhcHAudGhlbWVQYW5lbC5jbGFzcyArJyBbbmFtZT1cIicrIGFwcC50aGVtZVBhbmVsLmRhcmtNb2RlLmlucHV0TmFtZSArJ1wiXScpKTtcblx0ZWxtcy5tYXAoZnVuY3Rpb24oZWxtKSB7XG5cdFx0ZWxtLm9uY2hhbmdlID0gZnVuY3Rpb24oKSB7XG5cdFx0XHR2YXIgdGFyZ2V0Q29va2llID0gJyc7XG5cdFxuXHRcdFx0aWYgKHRoaXMuY2hlY2tlZCkge1xuXHRcdFx0XHRkb2N1bWVudC5xdWVyeVNlbGVjdG9yKCdodG1sJykuY2xhc3NMaXN0LmFkZChhcHAudGhlbWVQYW5lbC5kYXJrTW9kZS5jbGFzcyk7XG5cdFx0XHRcdHRhcmdldENvb2tpZSA9ICdkYXJrLW1vZGUnO1xuXHRcdFx0fSBlbHNlIHtcblx0XHRcdFx0ZG9jdW1lbnQucXVlcnlTZWxlY3RvcignaHRtbCcpLmNsYXNzTGlzdC5yZW1vdmUoYXBwLnRoZW1lUGFuZWwuZGFya01vZGUuY2xhc3MpO1xuXHRcdFx0fVxuXHRcdFx0QXBwLmluaXRWYXJpYWJsZSgpO1xuXHRcdFx0c2V0Q29va2llKGFwcC50aGVtZVBhbmVsLmRhcmtNb2RlLmNvb2tpZU5hbWUsIHRhcmdldENvb2tpZSk7XG5cdFx0XHRkb2N1bWVudC5kaXNwYXRjaEV2ZW50KG5ldyBDdXN0b21FdmVudChhcHAudGhlbWVQYW5lbC50aGVtZUxpc3Qub25DaGFuZ2VFdmVudCkpO1xuXHRcdH1cblx0fSk7XG5cdFxuXHQvLyBEYXJrIE1vZGUgQ29va2llXG5cdGlmIChnZXRDb29raWUoYXBwLnRoZW1lUGFuZWwuZGFya01vZGUuY29va2llTmFtZSkgJiYgZG9jdW1lbnQucXVlcnlTZWxlY3RvcignLicrIGFwcC50aGVtZVBhbmVsLmNsYXNzICsnIFtuYW1lPVwiJysgYXBwLnRoZW1lUGFuZWwuZGFya01vZGUuaW5wdXROYW1lICsnXCJdJykpIHtcblx0XHR2YXIgZWxtID0gZG9jdW1lbnQucXVlcnlTZWxlY3RvcignLicrIGFwcC50aGVtZVBhbmVsLmNsYXNzICsnIFtuYW1lPVwiJysgYXBwLnRoZW1lUGFuZWwuZGFya01vZGUuaW5wdXROYW1lICsnXCJdJyk7XG5cdFx0aWYgKGVsbSkge1xuXHRcdFx0ZWxtLmNoZWNrZWQgPSB0cnVlO1xuXHRcdFx0ZWxtLm9uY2hhbmdlKCk7XG5cdFx0fVxuXHR9XG59O1xuXG5cbi8qIEFwcGxpY2F0aW9uIENvbnRyb2xsZXJcbi0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLSAqL1xudmFyIEFwcCA9IGZ1bmN0aW9uICgpIHtcblx0XCJ1c2Ugc3RyaWN0XCI7XG5cdFxuXHRyZXR1cm4ge1xuXHRcdC8vbWFpbiBmdW5jdGlvblxuXHRcdGluaXQ6IGZ1bmN0aW9uICgpIHtcblx0XHRcdHRoaXMuaW5pdENvbXBvbmVudCgpO1xuXHRcdFx0dGhpcy5pbml0VmFyaWFibGUoKTtcblx0XHRcdHRoaXMuaW5pdEhlYWRlcigpO1xuXHRcdFx0dGhpcy5pbml0U2lkZWJhcigpO1xuXHRcdH0sXG5cdFx0aW5pdFNpZGViYXI6IGZ1bmN0aW9uKCkge1xuXHRcdFx0aGFuZGxlU2lkZWJhclNjcm9sbE1lbW9yeSgpO1xuXHRcdFx0aGFuZGxlU2lkZWJhck1pbmlmeUZsb2F0TWVudSgpO1xuXHRcdFx0aGFuZGxlU2lkZWJhck1lbnUoKTtcblx0XHRcdGhhbmRsZVNpZGViYXJNaW5pZnkoKTtcblx0XHRcdGhhbmRsZVNpZGViYXJNb2JpbGVUb2dnbGUoKTtcblx0XHRcdGhhbmRsZVNpZGViYXJNb2JpbGVEaXNtaXNzKCk7XG5cdFx0fSxcblx0XHRpbml0SGVhZGVyOiBmdW5jdGlvbigpIHtcblx0XHR9LFxuXHRcdGluaXRDb21wb25lbnQ6IGZ1bmN0aW9uKCkge1xuXHRcdFx0aGFuZGxlU2Nyb2xsYmFyKCk7XG5cdFx0XHRoYW5kbGVDYXJkQWN0aW9uKCk7XG5cdFx0XHRoYW5kZWxUb29sdGlwUG9wb3ZlckFjdGl2YXRpb24oKTtcblx0XHRcdGhhbmRsZVNjcm9sbFRvVG9wQnV0dG9uKCk7XG5cdFx0XHRoYW5kbGVTY3JvbGxUbygpO1xuXHRcdFx0aGFuZGxlVGhlbWVQYW5lbEV4cGFuZCgpO1xuXHRcdFx0aGFuZGxlVGhlbWVQYWdlQ29udHJvbCgpO1xuXHRcdH0sXG5cdFx0c2Nyb2xsVG9wOiBmdW5jdGlvbigpIHtcblx0XHRcdHdpbmRvdy5zY3JvbGxUbyh7dG9wOiAwLCBiZWhhdmlvcjogJ3Ntb290aCd9KTtcblx0XHR9LFxuXHRcdGdldENzc1ZhcmlhYmxlOiBmdW5jdGlvbih2YXJpYWJsZSkge1xuXHRcdFx0cmV0dXJuIHdpbmRvdy5nZXRDb21wdXRlZFN0eWxlKGRvY3VtZW50LmJvZHkpLmdldFByb3BlcnR5VmFsdWUodmFyaWFibGUpLnRyaW0oKTtcblx0XHR9LFxuXHRcdGluaXRWYXJpYWJsZTogZnVuY3Rpb24oKSB7XG5cdFx0XHRhcHAuY29sb3IudGhlbWUgICAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWFwcC10aGVtZScpO1xuXHRcdFx0YXBwLmZvbnQuZmFtaWx5ICAgICAgICAgID0gdGhpcy5nZXRDc3NWYXJpYWJsZSgnLS1icy1ib2R5LWZvbnQtZmFtaWx5Jyk7XG5cdFx0XHRhcHAuZm9udC5zaXplICAgICAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWJvZHktZm9udC1zaXplJyk7XG5cdFx0XHRhcHAuZm9udC53ZWlnaHQgICAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWJvZHktZm9udC13ZWlnaHQnKTtcblx0XHRcdGFwcC5jb2xvci5jb21wb25lbnRDb2xvciA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYXBwLWNvbXBvbmVudC1jb2xvcicpO1xuXHRcdFx0YXBwLmNvbG9yLmNvbXBvbmVudEJnICAgID0gdGhpcy5nZXRDc3NWYXJpYWJsZSgnLS1hcHAtY29tcG9uZW50LWJnJyk7XG5cdFx0XHRhcHAuY29sb3IuZGFyayAgICAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWRhcmsnKTtcblx0XHRcdGFwcC5jb2xvci5saWdodCAgICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtbGlnaHQnKTtcblx0XHRcdGFwcC5jb2xvci5ibHVlICAgICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtYmx1ZScpO1xuXHRcdFx0YXBwLmNvbG9yLmluZGlnbyAgICAgICAgID0gdGhpcy5nZXRDc3NWYXJpYWJsZSgnLS1icy1pbmRpZ28nKTtcblx0XHRcdGFwcC5jb2xvci5wdXJwbGUgICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtcHVycGxlJyk7XG5cdFx0XHRhcHAuY29sb3IucGluayAgICAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLXBpbmsnKTtcblx0XHRcdGFwcC5jb2xvci5yZWQgICAgICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtcmVkJyk7XG5cdFx0XHRhcHAuY29sb3Iub3JhbmdlICAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLW9yYW5nZScpO1xuXHRcdFx0YXBwLmNvbG9yLnllbGxvdyAgICAgICAgID0gdGhpcy5nZXRDc3NWYXJpYWJsZSgnLS1icy15ZWxsb3cnKTtcblx0XHRcdGFwcC5jb2xvci5ncmVlbiAgICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtZ3JlZW4nKTtcblx0XHRcdGFwcC5jb2xvci5zdWNjZXNzICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtc3VjY2VzcycpO1xuXHRcdFx0YXBwLmNvbG9yLnRlYWwgICAgICAgICAgID0gdGhpcy5nZXRDc3NWYXJpYWJsZSgnLS1icy10ZWFsJyk7XG5cdFx0XHRhcHAuY29sb3IuY3lhbiAgICAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWN5YW4nKTtcblx0XHRcdGFwcC5jb2xvci53aGl0ZSAgICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtd2hpdGUnKTtcblx0XHRcdGFwcC5jb2xvci5ncmF5ICAgICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtZ3JheScpO1xuXHRcdFx0YXBwLmNvbG9yLmxpbWUgICAgICAgICAgID0gdGhpcy5nZXRDc3NWYXJpYWJsZSgnLS1icy1saW1lJyk7XG5cdFx0XHRhcHAuY29sb3IuZ3JheTEwMCAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWdyYXktMTAwJyk7XG5cdFx0XHRhcHAuY29sb3IuZ3JheTIwMCAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWdyYXktMjAwJyk7XG5cdFx0XHRhcHAuY29sb3IuZ3JheTMwMCAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWdyYXktMzAwJyk7XG5cdFx0XHRhcHAuY29sb3IuZ3JheTQwMCAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWdyYXktNDAwJyk7XG5cdFx0XHRhcHAuY29sb3IuZ3JheTUwMCAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWdyYXktNTAwJyk7XG5cdFx0XHRhcHAuY29sb3IuZ3JheTYwMCAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWdyYXktNjAwJyk7XG5cdFx0XHRhcHAuY29sb3IuZ3JheTcwMCAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWdyYXktNzAwJyk7XG5cdFx0XHRhcHAuY29sb3IuZ3JheTgwMCAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWdyYXktODAwJyk7XG5cdFx0XHRhcHAuY29sb3IuZ3JheTkwMCAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWdyYXktOTAwJyk7XG5cdFx0XHRhcHAuY29sb3IuYmxhY2sgICAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWJsYWNrJyk7XG5cdFx0XHRcblx0XHRcdGFwcC5jb2xvci50aGVtZVJnYiAgICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYXBwLXRoZW1lLXJnYicpO1xuXHRcdFx0YXBwLmZvbnQuZmFtaWx5UmdiICAgICAgICAgID0gdGhpcy5nZXRDc3NWYXJpYWJsZSgnLS1icy1ib2R5LWZvbnQtZmFtaWx5LXJnYicpO1xuXHRcdFx0YXBwLmZvbnQuc2l6ZVJnYiAgICAgICAgICAgID0gdGhpcy5nZXRDc3NWYXJpYWJsZSgnLS1icy1ib2R5LWZvbnQtc2l6ZS1yZ2InKTtcblx0XHRcdGFwcC5mb250LndlaWdodFJnYiAgICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtYm9keS1mb250LXdlaWdodC1yZ2InKTtcblx0XHRcdGFwcC5jb2xvci5jb21wb25lbnRDb2xvclJnYiA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYXBwLWNvbXBvbmVudC1jb2xvci1yZ2InKTtcblx0XHRcdGFwcC5jb2xvci5jb21wb25lbnRCZ1JnYiAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYXBwLWNvbXBvbmVudC1iZy1yZ2InKTtcblx0XHRcdGFwcC5jb2xvci5kYXJrUmdiICAgICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtZGFyay1yZ2InKTtcblx0XHRcdGFwcC5jb2xvci5saWdodFJnYiAgICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtbGlnaHQtcmdiJyk7XG5cdFx0XHRhcHAuY29sb3IuYmx1ZVJnYiAgICAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWJsdWUtcmdiJyk7XG5cdFx0XHRhcHAuY29sb3IuaW5kaWdvUmdiICAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWluZGlnby1yZ2InKTtcblx0XHRcdGFwcC5jb2xvci5wdXJwbGVSZ2IgICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtcHVycGxlLXJnYicpO1xuXHRcdFx0YXBwLmNvbG9yLnBpbmtSZ2IgICAgICAgICAgID0gdGhpcy5nZXRDc3NWYXJpYWJsZSgnLS1icy1waW5rLXJnYicpO1xuXHRcdFx0YXBwLmNvbG9yLnJlZFJnYiAgICAgICAgICAgID0gdGhpcy5nZXRDc3NWYXJpYWJsZSgnLS1icy1yZWQtcmdiJyk7XG5cdFx0XHRhcHAuY29sb3Iub3JhbmdlUmdiICAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLW9yYW5nZS1yZ2InKTtcblx0XHRcdGFwcC5jb2xvci55ZWxsb3dSZ2IgICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMteWVsbG93LXJnYicpO1xuXHRcdFx0YXBwLmNvbG9yLmdyZWVuUmdiICAgICAgICAgID0gdGhpcy5nZXRDc3NWYXJpYWJsZSgnLS1icy1ncmVlbi1yZ2InKTtcblx0XHRcdGFwcC5jb2xvci5zdWNjZXNzUmdiICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtc3VjY2Vzcy1yZ2InKTtcblx0XHRcdGFwcC5jb2xvci50ZWFsUmdiICAgICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtdGVhbC1yZ2InKTtcblx0XHRcdGFwcC5jb2xvci5jeWFuUmdiICAgICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtY3lhbi1yZ2InKTtcblx0XHRcdGFwcC5jb2xvci53aGl0ZVJnYiAgICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtd2hpdGUtcmdiJyk7XG5cdFx0XHRhcHAuY29sb3IuZ3JheVJnYiAgICAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWdyYXktcmdiJyk7XG5cdFx0XHRhcHAuY29sb3IubGltZVJnYiAgICAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWxpbWUtcmdiJyk7XG5cdFx0XHRhcHAuY29sb3IuZ3JheTEwMFJnYiAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWdyYXktMTAwLXJnYicpO1xuXHRcdFx0YXBwLmNvbG9yLmdyYXkyMDBSZ2IgICAgICAgID0gdGhpcy5nZXRDc3NWYXJpYWJsZSgnLS1icy1ncmF5LTIwMC1yZ2InKTtcblx0XHRcdGFwcC5jb2xvci5ncmF5MzAwUmdiICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtZ3JheS0zMDAtcmdiJyk7XG5cdFx0XHRhcHAuY29sb3IuZ3JheTQwMFJnYiAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWdyYXktNDAwLXJnYicpO1xuXHRcdFx0YXBwLmNvbG9yLmdyYXk1MDBSZ2IgICAgICAgID0gdGhpcy5nZXRDc3NWYXJpYWJsZSgnLS1icy1ncmF5LTUwMC1yZ2InKTtcblx0XHRcdGFwcC5jb2xvci5ncmF5NjAwUmdiICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtZ3JheS02MDAtcmdiJyk7XG5cdFx0XHRhcHAuY29sb3IuZ3JheTcwMFJnYiAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWdyYXktNzAwLXJnYicpO1xuXHRcdFx0YXBwLmNvbG9yLmdyYXk4MDBSZ2IgICAgICAgID0gdGhpcy5nZXRDc3NWYXJpYWJsZSgnLS1icy1ncmF5LTgwMC1yZ2InKTtcblx0XHRcdGFwcC5jb2xvci5ncmF5OTAwUmdiICAgICAgICA9IHRoaXMuZ2V0Q3NzVmFyaWFibGUoJy0tYnMtZ3JheS05MDAtcmdiJyk7XG5cdFx0XHRhcHAuY29sb3IuYmxhY2tSZ2IgICAgICAgICAgPSB0aGlzLmdldENzc1ZhcmlhYmxlKCctLWJzLWJsYWNrLXJnYicpO1xuXHRcdH1cblx0fTtcbn0oKTtcblxuZG9jdW1lbnQuYWRkRXZlbnRMaXN0ZW5lcignRE9NQ29udGVudExvYWRlZCcsIGZ1bmN0aW9uKCkge1xuXHRBcHAuaW5pdCgpO1xufSk7Il19
