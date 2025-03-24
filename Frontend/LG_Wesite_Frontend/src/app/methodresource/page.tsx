"use client";
import Image from "next/image";
import methodImage from './../../app/images/methodresources.png'
import AgileModelAccordion from "./../../components/AgileModel"; 
import { useState } from 'react';
import '../css/pages/_methodresource.scss';
import '../css/abstracts/_variables.scss';
import AppliedTool from "./../../components/Appliedtools";
export default function About() {
  const [activeAccordion, setActiveAccordion] = useState(null);

  const toggleAccordion = (index) => {
    if (activeAccordion === index) {
      setActiveAccordion(null);
    } else {
      setActiveAccordion(index);
    }
  };
  return (
    <div className="method-page">
      <div className="method-header">
      <div className="image-overlay">
          <Image
            src={methodImage}
            alt="About Background"
            className="background-image" />
        </div>
        <div className="overlay-text">Method & Resources</div>
      </div>
    
      <AgileModelAccordion />
     
      <AppliedTool showButton={false} isHomePage={false} />
    
    </div>
  );
}
