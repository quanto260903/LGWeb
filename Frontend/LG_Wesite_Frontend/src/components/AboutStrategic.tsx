'use client';

import React, { useState, useEffect } from 'react';
import Link from 'next/link';
import Image from 'next/image';
import '../app/css/abstracts/_variables.scss';
import '../app/css/components/_buttons.scss';
import '../app/css/pages/_about.scss';
import strategyimage from '../app/images/strategy-image.png';

import { BlogCategoryType } from '@/type/BlogCategory';
import { GetAboutStrategic } from '@/api/about';

const AboutUs = ({ showButton = true }) => {
  const [data, setData] = useState<BlogCategoryType[]>([]);

  useEffect(() => {
    const loadData = async () => {
      const result = await GetAboutStrategic();
      setData(result);
    };
    loadData();
  }, []);

  return (
    <>
      <section className="strategic-approaches-section">
        <div className="approach-container wrp">
          <div className="approach-image">
            <Image
              src={strategyimage} 
              alt="Strategic Approaches"
              className="strategy-image"
              layout="responsive" 
            />
            
          </div>

          <div className="approach-details">
            <h1 className="approach-title tl2">Strategic approaches</h1>
            {data && data.map((item, index) => (
              <div key={index} className="approach-item">
                <div className="icon-image tl41">
                  <img
                    src={item.imageUrl || '/picture/NoImage.png'}
                    style={{ width: '100%', height: 'auto' }}
                    alt={item.name || 'Image'}
                  />
                </div>
                <div className='text-strateic'>
                  <h2 className='tl41'>{item.name}</h2>
                  <p className='tl4'>{item.description}</p>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>
    </>
  );
};

export default AboutUs;
