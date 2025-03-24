'use client';

import React, { useState, useEffect } from 'react';
import Link from 'next/link';
import Image from 'next/image';
import logoImage from './../app/images/LGlogo.png';
import logoResponsive from './../app/images/LogoResponsive.png';
import logoResponsiveR from './../app/images/logoResponsiveR.png';
import '../app/css/abstracts/_variables.scss';

import { MenuType } from '@/type/Menu';
import { getMenuData } from '@/api/menu';
export default function Header  ()  {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const [data, setData] = useState<MenuType[]>([]);

  useEffect(() => {
    const loadData = async () => {
      const result = await getMenuData();
      setData(result);
    };
    loadData();
  }, []);
  const toggleMenu = () => {
    setIsMenuOpen(!isMenuOpen);
  };

  // Function to close menu after clicking a link
  const closeMenu = () => {
    setIsMenuOpen(false);
  };
  const renderMenuItems = (items: MenuType[], parentId: number = 0) => {
    // Lấy các menu con có parentId tương ứng
    const children = items.filter((item) => item.parentId === parentId);
  
    return (
      <ul className={parentId !== 0 ? 'dn' : ''}>
        {children.map((item) => (
          <li key={item.id}>
            <Link className='tl4' href={item.linkUrl || '#'} onClick={closeMenu}>
              {item.name}
            </Link>
            {/* Nếu item hiện tại có các menu con, gọi lại hàm renderMenuItems cho menu con đó */}
            {items.some((child) => child.parentId === item.id) && (
              renderMenuItems(items, item.id) // Đệ quy cho menu con
            )}
          </li>
        ))}
      </ul>
    );
  };
  return (
    
    <div className={`header ${isMenuOpen ? 'menu-open' : ''}`}>
      <div className='wrp'>
        <div className='logo'>
          <Link href={'#'} onClick={closeMenu}>
            <Image src={logoImage} width={120} height={10} alt='' className='logoImage' />
          </Link>
          <Link href={'#'} onClick={closeMenu}>
            <Image src={logoResponsive} className='logoResponsive' alt='' />
          </Link>
        </div>
        <div className="menu-toggle" onClick={toggleMenu}>
          <Link href={'#'}>
            <Image src={logoResponsiveR} className='logoResponsive' alt='' />
          </Link>
        </div>
        <div className={`nav ${isMenuOpen ? 'show-menu' : 'hide-menu'}`}>
        {renderMenuItems(data, 0)}
        </div>
      </div>
    </div>
  );
};

