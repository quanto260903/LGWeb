'use client';
 
import React, { useState, useEffect } from 'react';
import Link from 'next/link'
import Image from 'next/image'; // Import thư viện next/image
 
import aboutimages from './../../app/images/about.png';
import Team from "./../../components/Team";
import AboutUs from "./../../components/AboutUs";
import AboutStrategic from "./../../components/AboutStrategic";
import AboutCeo from "./../../components/AboutCeo";
 
import '../css/abstracts/_variables.scss';
import '../css/pages/_about.scss';
 

const AboutPage: React.FC = () => {

  return (
    <><div className="about-container-img" >
      <div className="image-overlay"> 
        <Link href={'/'}>
          <Image
            src={aboutimages}
            alt="About Background"
            className="background-image" />
        </Link>
      </div>
      <div className="overlay-text">About Us</div>
    </div>
      <AboutUs showButton={false} />

      <AboutStrategic />

      <AboutCeo />

      <Team />

    </>
  );
};
export default AboutPage;
