'use client';
import React, { useState } from 'react';
import Link from 'next/link';
import Image from 'next/image';
import emailicon from './../app/images/icon-email.png';
import phoneicon from './../app/images/icon-phone.png';
import locationicon from './../app/images/icon-location.png';
import emaillogo from './../app/images/logo-email.png';
import dropdownIcon from './../app/images/dropdownIcon.png';

const Footer = () => {
  const [isCompanyOpen, setIsCompanyOpen] = useState(false);
  const [isAboutOpen, setIsAboutOpen] = useState(false);
  const [isLocationOpen, setIsLocationOpen] = useState(false);
  return (
    <footer className="footer">
      <div className="footer-container wrp">
        <div className="icon">
          <Link href={'/'}>
            <Image src={emaillogo} alt="Contact icon" width={40} height={40} />
          </Link>
        </div>
      </div>

      <div className="footer-content wrp">
        <div className="footer-section contact-us">
          <h3 className='tl3'>Contact us</h3>
          <p className='tl4'>
            Connect with us and let us help you achieve your goals. Our team is ready to provide you with exceptional service and support.
          </p>
          <div className="email-container">
            <input type="email" placeholder="Enter your email" className="email-input" />
            <button className="email-button tl4">Submit</button>
          </div>
        </div>

        <div className="footer-section company" id='footer-section' >
          <div className="section-title" onClick={() => setIsCompanyOpen(!isCompanyOpen)}>
            <h3 className='tl3'>Company</h3>
            <div className='dropdownIcon'><Image src={dropdownIcon}  alt="Dropdown Icon" /></div>
          </div>
          <div className={`collapsible-content ${isCompanyOpen ? 'expanded' : ''}`}>
            <ul>
              <li className='tl4'><a href="#" className='footer-section-menu'>Home</a></li>
              <li className='tl4'><a href="#" className='footer-section-menu'>Service</a></li>
              <li className='tl4'><a href="#" className='footer-section-menu'>Products</a></li>
              <li className='tl4'><a href="#" className='footer-section-menu'>About</a></li>
            </ul>
          </div>
        </div>

        <div className="footer-section company" id='footer-section'>
          <div className="section-title" onClick={() => setIsAboutOpen(!isAboutOpen)}>
            <h3 className='tl3'>About</h3>
            <div className='dropdownIcon'><Image src={dropdownIcon} alt="Dropdown Icon" /></div>
          </div>
          <div className={`collapsible-content ${isAboutOpen ? 'expanded' : ''}`}>
            <ul>
              <li className='tl4'><a href="#" className='footer-section-menu'>Method & resources</a></li>
              <li className='tl4'><a href="#" className='footer-section-menu'>Contacts</a></li>
              <li className='tl4'><a href="#" className='footer-section-menu'>Careers</a></li>
            </ul>
          </div>
        </div>

        <div className="footer-section" id='footer-section'>
          <div className="section-title" onClick={() => setIsLocationOpen(!isLocationOpen)}>
            <h3 className='tl3'> Our location </h3>
            <div className='dropdownIcon'><Image src={dropdownIcon}  alt="Dropdown Icon" /></div>
          </div>
          <div className={`collapsible-content ${isLocationOpen ? 'expanded' : ''}`}>
            <li className="footer-section-icon tl4">
              <Link href={'/'}>
                <Image src={locationicon} alt="Location icon"  width={30} height={20}/>
              </Link>
              <p>9th Floor, Heid Building, Lane 12 Lang Ha, Ba Dinh District, Ha Noi, Viet Nam</p>
            </li>
            <li className="footer-section-icon tl4">
              <Link href={'/'}>
                <Image src={phoneicon} alt="Phone icon" />
              </Link>
              <p>(+84) 24 3514 0868</p>
            </li>
            <li className="footer-section-icon tl4">
              <Link href={'/'}>
                <Image src={emailicon} alt="Email icon" />
              </Link>
              <p>Email: info@aod.vn</p>
            </li>
          </div>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
