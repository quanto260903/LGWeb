"use client";
import Image from "next/image";
import tfsIcon from './../app/images/tfs2018.png'
import './../../src/app/css/pages/_methodresource.scss';
import '../app/css/components/_buttons.scss';
import Link from 'next/link';
import iconDropdowm from './../app/images/iconDropdown.png'
import React, { useState, useEffect } from 'react';

import { BlogCategoryType,ActiveAccord } from '@/type/BlogCategory';
import { GetAppliedTools } from '@/api/methodresource';

interface AppliedToolsProps {
  isHomePage: boolean;
  showButton?: boolean;
}
export default function Appliedtools({ isHomePage, showButton = true }: AppliedToolsProps) {
  
  const [activeAccordion, setActiveAccordion] = useState(null);
  const [data, setData] = useState<BlogCategoryType[] | null>(null);

  const toggleAccordion = (index) => {
        setActiveAccordion(activeAccordion === index ? null : index);
    };
  useEffect(() => {
    const loadData = async () => {
      const result = await GetAppliedTools();
      setData(result || []);
    };
    loadData();
  }, []);


  
  return (
    <div className={`method-page ${isHomePage ? 'homepage-style' : ''}`}>
      <div className={`applied-tools-section ${isHomePage ? 'tool-background' : ''}`}>
        {isHomePage ? (
          <p className={`tl ${isHomePage ? 'extra-class' : ''}`}>\ Applied tools \</p>
        ) : (
          <>
            <p className="tl">\ Method & Resources \</p>
            <p className="tl2">Applied tools</p>
          </>
        )}
        
        <div className="tools-grid wrp">
          {data && data.map((item, index) => (
            <div
              key={index}
              className={`tool-card ${activeAccordion === index ? "expanded" : ""}`}
              onClick={() => toggleAccordion(index)}
            >
              {
                    data && <img
                        src={item.imageUrl || '/picture/NoImage.png'}
                        alt={item.imageUrl || 'Image'}
                        className="image-tool"
                    />
                }
              <div className="tool-card-text">
                <div className="section-title">
                  <h5 className="tl5">{item.name}</h5>
                  {isHomePage && (
                    <Image
                      src={iconDropdowm}
                      alt="Dropdown Icon"
                      className="dropdown-icon"
                    />
                  )}
                </div>
                <p className="tl4">{item.description}</p>
              </div>
            </div>
          ))}
          
        </div>
      </div>
    </div>
  );
}