'use client';
import Image from "next/image";
import Link from 'next/link';
import '../app/css/components/_buttons.scss';
import './../../src/app/css/pages/_services.scss';
import outsourceImage from './../app/images/outsource.png'
import supportImage from './../app/images/support.png'
import '../app/css/abstracts/_variables.scss';
import React, { useState, useEffect } from 'react';
import { BlogCategoryType } from '@/type/BlogCategory';
import { GetOurExpertise } from '@/api/service';
interface OurExpertisesProps {
  isHomePage: boolean;
  showButton?: boolean;
}
export default function Services({ isHomePage, showButton = true }: OurExpertisesProps) {
  const [data, setData] = useState<BlogCategoryType[]>([]);

  useEffect(() => {
    const loadData = async () => {
      const result = await GetOurExpertise();
      setData(result);
    };
    loadData();
  }, []);
  return (
    <div className={`services-page ${isHomePage ? 'home-background' : ''}`}>
      <div className="service-content wrp">
        {/* Expertise Section */}
        <p className="tl">\ Services \</p>
        <p className="tl2">Our Expertise</p>
        <div className="service-description">
        {data && data.map((item, index) => (
  item.order % 2 !== 0 ? (
    <div key={index} className="expertise-container">
      {/* Software Development Section */}
      <div className="software-development">
        <p className="tl44">{item.name}</p>
        <p className="body1">{item.description}</p>
        
        {showButton && (
          <div className="button-container">
            <Link href={'/about'}>
              <button className="read-more-small-btn">Read more</button>
            </Link>
          </div>
        )}
      </div>

      {/* Image Section */}
      <div className="software-image">
        <Image
          src={outsourceImage}
          alt="Software development"
          width={620}
          height={400}
        />
      </div>
    </div>
  ) : (
    <div key={index} className="support-section">
      <div className="support-text">
        <p className="tl44">{item.name}</p>
        <p className="body1">{item.description}</p>

        <div className="contact-support" style={{ display: isHomePage ? 'none' : 'flex' }}>
          <p className="contact-title">Contact our Support team:</p>
          <p className="contact-body">
            Vietnam: (+84) 28 25134 086<br />
            Denmark: +44 203 808 3269<br />
            Europe: +31 (202)-654-465<br />
            North America: +45 7344 7002
          </p>
        </div>

        {showButton && (
          <div className="button-container">
            <Link href={'/about'}>
              <button className="read-more-small-btn">Read more</button>
            </Link>
          </div>
        )}
      </div>

      {/* Image Section */}
      <div className="support-image">
        <Image
          src={supportImage}
          alt="Support team"
          width={620}
          height={400}
        />
      </div>
    </div>
  )
))}
        
        </div>
      </div>
    </div>
  );
}
